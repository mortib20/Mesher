using System.Text;
using Mesher.Database;
using Mesher.Mesh.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;

namespace Mesher.Mesh;

// ReSharper disable once InconsistentNaming
public class MeshServiceMQTT(ILogger<MeshServiceMQTT> logger, IOptions<MeshServiceMQTTConfig> configInjected, IServiceProvider serviceProvider) : Global.Service.AviatorBackgroundService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = configInjected.Value; 
        var mqttFactory = new MqttClientFactory();

        using var mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(config.Server).Build();
        
        logger.LogInformation("Connected to {Server} MQTT-Server.", config.Server);

        mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            await using var serviceScope = serviceProvider.CreateAsyncScope();
            await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<MesherContext>();

            logger.LogInformation("Received application message");
            var mqttPayload = e.ApplicationMessage.Payload;
            var meshJson = Encoding.UTF8.GetString(mqttPayload);
            // var meshPayload = JsonSerializer.Deserialize<MeshPayload>(mqttPayload.ToArray());

            var entry = new DbMeshMessage
            {
                RawMessage = meshJson
            };

            // Maybe move this to a handle via some pipe
            await dbContext.MeshMessages.AddAsync(entry, stoppingToken);
            await dbContext.SaveChangesAsync(stoppingToken);
        };

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(config.Topic).Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        logger.LogInformation("MQTT client subscribed to {topic} topic.", config.Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(100, stoppingToken).ConfigureAwait(false);
        }
    }
}
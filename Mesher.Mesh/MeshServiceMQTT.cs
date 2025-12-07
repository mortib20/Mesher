using System.Text;
using Mesher.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTTnet;

namespace Mesher.Mesh;

// ReSharper disable once InconsistentNaming
public class MeshServiceMQTT(ILogger<MeshServiceMQTT> logger, IServiceProvider serviceProvider) : Global.Service.AviatorBackgroundService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var mqttFactory = new MqttClientFactory();

        using var mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("192.168.168.3").Build();

        mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            await using var serviceScope = serviceProvider.CreateAsyncScope();
            await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<MesherContext>();
            
            logger.LogInformation("Received application message");
            var mqttPayload = e.ApplicationMessage.Payload;
            var meshJson = Encoding.UTF8.GetString(mqttPayload);
            // var meshPayload = JsonSerializer.Deserialize<MeshPayload>(mqttPayload.ToArray());

            var entry = new DbMeshMessage()
            {
                RawMessage = meshJson
            };

            // Maybe move this to a handle via some pipe
            await dbContext.MeshMessages.AddAsync(entry, stoppingToken);
            await dbContext.SaveChangesAsync(stoppingToken);
        };
        
        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter("msh/2/json/#").Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        
        logger.LogInformation("MQTT client subscribed to topic.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(100, stoppingToken).ConfigureAwait(false);
        }
    }
}
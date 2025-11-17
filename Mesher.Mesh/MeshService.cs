using System.Buffers;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Mesher.Mesh.Models;
using Microsoft.Extensions.Logging;
using MQTTnet;

namespace Mesher.Mesh;

public class MeshService(ILogger<MeshService> logger) : Global.Service.AviatorBackgroundService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var mqttFactory = new MqttClientFactory();

        using var mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("192.168.168.3").Build();

        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            logger.LogInformation("Received application message");
            var mqttPayload = e.ApplicationMessage.Payload;
            var meshPayload = JsonSerializer.Deserialize<MeshPayload>(mqttPayload.ToArray());
            
            Console.WriteLine();
            
            return Task.CompletedTask;
        };

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter("msh/2/json/#").Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        
        logger.LogInformation("MQTT client subscribed to topic.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }
}
using System.Text;
using Mesher.Global.Service;
using Meshtastic.Connections;
using Meshtastic.Data;
using Meshtastic.Data.MessageFactories;
using Meshtastic.Protobufs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mesher.Mesh;

public class MeshServiceDirect(ILogger<MeshServiceDirect> logger) : AviatorBackgroundService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var deviceConnection = new TcpConnection(logger, "192.168.2.128");
        
        var wantConfigMessage = deviceConnection.ToRadioFactory.CreateWantConfigMessage();

        deviceConnection.DeviceStateContainer.GetDeviceNodeInfo();
        var state = await deviceConnection.WriteToRadio(wantConfigMessage, (s, container) =>
        {
            if (s.PayloadVariantCase != FromRadio.PayloadVariantOneofCase.ConfigCompleteId)
            {
                return Task.FromResult(false);
            }
                
            logger.LogInformation("Payload {Variant}", s.PayloadVariantCase);

            deviceConnection.ReadFromRadio((FromRadio radio, DeviceStateContainer container) =>
            {
                if (radio.PayloadVariantCase == FromRadio.PayloadVariantOneofCase.Packet)
                {
                    if (radio.Packet.Decoded.Payload.ToByteArray().Length != 0 && radio.Packet.Decoded.Portnum == PortNum.TextMessageApp)
                    {
                        var text = Encoding.UTF8.GetString(radio.Packet.Decoded.Payload.ToByteArray());
                        logger.LogInformation("Received {text}", text);
                    }
                
                    return Task.FromResult(false);
                }
            
                logger.LogInformation("Payload {Variant}", radio.PayloadVariantCase);
            
                Task.Delay(100, stoppingToken).Wait(stoppingToken);
                
                return Task.FromResult(false);
            }).Wait(stoppingToken);
            
            return Task.FromResult(true);
        });
        deviceConnection.Disconnect();

        
        
        // (radio, container) =>
        // {
        //     Console.WriteLine();
        //     return new Task<bool>(() => true);
        // }

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
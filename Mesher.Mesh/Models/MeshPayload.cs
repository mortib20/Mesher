using System.Text.Json.Serialization;
using Mesher.Mesh.Models.Converter;

namespace Mesher.Mesh.Models;

public class MeshPayload
{
    [JsonPropertyName("channel")]
    public int Channel { get; set; }
    
    [JsonPropertyName("hop_start")]
    public int HopStart { get; set; }
    
    [JsonPropertyName("hops_away")]
    public int HopsAway { get; set; }
    
    [JsonPropertyName("rssi")]
    public decimal Rssi { get; set; }
    
    [JsonPropertyName("snr")]
    public decimal Snr { get; set; }
    
    [JsonPropertyName("sender")]
    public required string Sender { get; set; }

    public string SenderId => Sender.Remove(0, 1);
    
    [JsonPropertyName("id")]
    public long Id { get; set; }
    public string NodeId => Id.ToString("X").ToLowerInvariant();
    
    [JsonPropertyName("from")]
    public long From { get; set; }
    public string FromId => From.ToString("X").ToLowerInvariant(); 
    
    [JsonPropertyName("to")]
    public long To { get; set; }
    public string ToId => To.ToString("X").ToLowerInvariant(); 
    
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    public DateTimeOffset DateTimeOffset => DateTimeOffset.FromUnixTimeSeconds(Timestamp);


    [JsonPropertyName("type")]
    [JsonConverter(typeof(LowercaseEnumConverter<MeshPayloadType>))]
    public MeshPayloadType Type { get; set; }
}
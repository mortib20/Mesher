using System.ComponentModel.DataAnnotations;

namespace Mesher.Mesh.Config;

// ReSharper disable once InconsistentNaming
public class MeshServiceMQTTConfig
{
    public static string Section = "MeshServiceMQTT";
    [Required]
    public required string Server { get; init; }
    [Required]
    public required string Topic { get; set; }
}
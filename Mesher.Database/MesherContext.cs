using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Mesher.Database;

public class MesherContext(DbContextOptions<MesherContext> options) : DbContext(options)
{
    public DbSet<DbMeshMessage> MeshMessages { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbMeshMessage>()
            .HasKey(s => s.EntryId);
    }
}

public class DbMeshMessage
{
    [Key]
    public Guid EntryId { get; init; } = Guid.NewGuid();

    public Instant CreatedAt { get; init; } = SystemClock.Instance.GetCurrentInstant();
    
    [Column(TypeName = "jsonb")]
    [MaxLength(8192)]
    public required string RawMessage { get; init; }
}
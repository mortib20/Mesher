using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Mesher.Database;

public class MesherContext(DbContextOptions<MesherContext> options) : DbContext(options)
{
    public DbSet<DbMeshMessage> MeshMessages { get; init; }
    public DbSet<DbMeshHardware> MeshHardwares { get; init; }
    // public DbSet<DbMeshNodeView> DbMeshNodeView { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbMeshMessage>()
            .HasKey(s => s.EntryId);

        modelBuilder.Entity<DbMeshHardware>(ent =>
        {
            ent.HasKey(e => e.Key);
            ent.Property(e => e.Key)
                .ValueGeneratedNever();
        });
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

public class DbMeshHardware
{
    [Key]
    public int Key { get; init; }
    
    [MaxLength(8192)]
    public required string Name { get; init; }
}

[Keyless]
public class DbMeshNodeView
{
    
}
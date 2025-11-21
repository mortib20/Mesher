using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mesher.Database;

public class MesherContext(DbContextOptions<MesherContext> options) : DbContext(options)
{
    public DbSet<DbMeshMessage> MeshMessages { get; init; }
    public DbSet<DbMeshHardware> MeshHardwares { get; init; }
    
    // Views
    public DbSet<DbMeshNodeInfo> MeshNodeInfo { get; init; }
    public DbSet<DbMeshPosition> MeshPositions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // MeshMessages
        modelBuilder.Entity<DbMeshMessage>(eb =>
        {
            eb.Property<long>("From")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'from' AS bigint))", stored: true);

            eb.HasIndex("From")
                .HasDatabaseName("IX_MeshMessages_From");
            
            eb.Property<long>("To")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'to' AS bigint))", stored: true);

            eb.HasIndex("To")
                .HasDatabaseName("IX_MeshMessages_To");
            
            eb.Property<string>("Type")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'type' AS text))", stored: true);
            
            eb.HasIndex("Type")
                .HasDatabaseName("IX_MeshMessages_Type");
            
            // Other computed
            
            eb.Property<double?>("SNR")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'snr' AS double precision))", stored: true);
            
            eb.Property<double?>("RSSI")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'rssi' AS double precision))", stored: true);
            
            eb.Property<long>("Channel")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'channel' AS bigint))", stored: true);
            
            eb.Property<long?>("HopsAway")
                .HasComputedColumnSql("(CAST(\"RawMessage\" ->> 'hops_away' AS bigint))", stored: true);
            
            eb.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_MeshMessages_CreatedAt_DESC")
                .HasAnnotation("Npgsql:IndexSortOrder", new[] { SortOrder.Descending });
        });

        // MeshHardwares
        modelBuilder.Entity<DbMeshHardware>(eb =>
        {
            eb.HasKey(e => e.Key);
            eb.Property(e => e.Key)
                .ValueGeneratedNever();
        });

        // MeshNodeInfo
        modelBuilder.Entity<DbMeshNodeInfo>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("MeshNodeInfo");
        });
        
        modelBuilder.Entity<DbMeshPosition>(eb =>
        {
            eb.HasNoKey();
            eb.ToView("MeshPosition");
        });
    }
}

public class DbMeshMessage
{
    [Key]
    public Guid EntryId { get; init; } = Guid.NewGuid();

    public Instant CreatedAt { get; init; } = SystemClock.Instance.GetCurrentInstant();

    // TODO add other computed fields
    
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
public class DbMeshNodeInfo
{
    public int Id { get; init; }

    public required string LongName { get; init; }
    public required string ShortName { get; init; }
    
    public int Role { get; init; }
    public required string Hardware { get; init; }
    
    public int HopsAway { get; init; }

    public Instant LastSeen { get; init; }
}

[Keyless]
public class DbMeshPosition
{
    public int From { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public int? Altitude { get; init; }
    public int PrecisionBits { get; init; }
    public Instant? Time { get; init; }
    public Instant CreatedAt { get; init; }
}
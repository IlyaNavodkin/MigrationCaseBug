using Microsoft.EntityFrameworkCore;

namespace MigrationCaseBug.Data;

public class TestContext : DbContext
{
    public DbSet<Object> Objects { get; set; }
    public DbSet<Metadata> Metadatas { get; set; }

    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API конфигурация

        modelBuilder.Entity<Object>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Metadata>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Object>()
            .HasOne(u => u.Metadata)
            .WithOne()
            .HasForeignKey<Object>(u => u.MetadataId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}



public class Object
{
    public string Id { get; set; }

    public Metadata Metadata { get; set; }
    public string MetadataId { get; set; }

}

public class Metadata
{
    public string Id { get; set; }
}

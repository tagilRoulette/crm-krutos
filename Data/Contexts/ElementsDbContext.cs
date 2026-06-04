using Crm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class ElementsDbContext : DbContext
{
    public ElementsDbContext(DbContextOptions<ElementsDbContext> options) : base(options) { }
    public DbSet<ElementEntity> ElementEntity => Set<ElementEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElementEntity>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Json)
            .IsRequired();

            entity.Property(x => x.LastModified)
                .IsRequired();

            entity.HasOne(x => x.Page)
            .WithMany(z => z.Elements)
            .HasForeignKey(z => z.PageId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
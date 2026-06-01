using Crm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class ElementsDbContext : DbContext
{
    public ElementsDbContext(DbContextOptions<ElementsDbContext> options) : base(options) { }
    public DbSet<ElementEntity> Elements => Set<ElementEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElementEntity>(entity =>
        {
            //entity.ToTable("crm_elements");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Json)
            .HasColumnName("json")
            .IsRequired();

            entity.Property(x => x.LastModified)
                .HasColumnName("last_modified")
                .IsRequired();

            entity.HasOne(x => x.Page)
            .WithMany(z => z.Elements)
            .HasForeignKey(z => z.PageId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
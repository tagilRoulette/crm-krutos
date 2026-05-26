using Crm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class ElementsDbContext : DbContext
{
    public ElementsDbContext(DbContextOptions<ElementsDbContext> options) : base(options) { }
    public DbSet<CrmElementEntity> Elements => Set<CrmElementEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrmElementEntity>(entity =>
        {
            entity.ToTable("crm_elements");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Json)
            .HasColumnName("json")
            .IsRequired();

            entity.Property(x => x.LastModified)
                .HasColumnName("last_modified")
                .IsRequired();

            entity.HasOne(x => x.Project)
            .WithMany(z => z.Elements)
            .HasForeignKey(z => z.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
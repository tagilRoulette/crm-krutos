using Crm.Data.Entities;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class PagesDbContext : DbContext
{
    public PagesDbContext(DbContextOptions<PagesDbContext> options) : base(options) { }
    public DbSet<PageEntity> PageEntity => Set<PageEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PageEntity>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
            .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.HasOne(x => x.Project)
            .WithMany(z => z.Pages)
            .HasForeignKey(z => z.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Elements)
            .WithOne(x => x.Page)
            .HasForeignKey(z => z.PageId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
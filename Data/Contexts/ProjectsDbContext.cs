using Crm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class ProjectsDbContext : DbContext
{
    public ProjectsDbContext(DbContextOptions<ElementsDbContext> options) : base(options) { }
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.ToTable("projects");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.NavigationType)
                .HasColumnName("navigation_type")
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.Property(x => x.Elements)
                .HasColumnName("elements");
        });

        base.OnModelCreating(modelBuilder);
    }
}

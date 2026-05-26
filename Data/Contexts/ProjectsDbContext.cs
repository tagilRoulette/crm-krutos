using Crm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data.Contexts;

public class ProjectsDbContext : DbContext
{
    public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base(options) { }
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

            entity.HasMany(p => p.Elements)
              .WithOne(e => e.Project)
              .HasForeignKey(e => e.ProjectId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CrmElementEntity>(entity =>
        {
            entity.ToTable("crm_elements"); 
            entity.HasKey(x => x.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}
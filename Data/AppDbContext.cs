using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CrmElement> Elements => Set<CrmElement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrmElement>(entity =>
        {
         
            entity.ToTable("crm_elements");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.X)
            .HasColumnName("x_coordinate")
            .IsRequired(); 

            entity.Property(x => x.Y)
            .HasColumnName("y_coordinate")
            .IsRequired(); 

            entity.Property(x => x.LastModified)
                .HasColumnName("last_modified")
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
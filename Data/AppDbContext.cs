using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CrmElement> Elements => Set<CrmElement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrmElement>(entity =>
        {
<<<<<<< HEAD
            entity.ToTable("users");
=======
         
            entity.ToTable("crm_elements");
>>>>>>> 3e3e200 (Drag & drop WIP.)

            entity.HasKey(x => x.Id);

            entity.Property(x => x.X)
            .HasColumnName("x_coordinate")
<<<<<<< HEAD
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(x => x.Y)
            .HasColumnName("y_coordinate")
            .HasMaxLength(200)
            .IsRequired();
=======
            .IsRequired(); 

            entity.Property(x => x.Y)
            .HasColumnName("y_coordinate")
            .IsRequired(); 
>>>>>>> 3e3e200 (Drag & drop WIP.)

            entity.Property(x => x.LastModified)
                .HasColumnName("last_modified")
                .IsRequired();
        });

<<<<<<< HEAD

=======
>>>>>>> 3e3e200 (Drag & drop WIP.)
        base.OnModelCreating(modelBuilder);
    }
}
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
<<<<<<< HEAD
            entity.ToTable("users");
=======
         
            entity.ToTable("crm_elements");
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
         
            entity.ToTable("crm_elements");
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2

            entity.HasKey(x => x.Id);

            entity.Property(x => x.X)
            .HasColumnName("x_coordinate")
<<<<<<< HEAD
<<<<<<< HEAD
            .HasMaxLength(200)
            .IsRequired();

            entity.Property(x => x.Y)
            .HasColumnName("y_coordinate")
            .HasMaxLength(200)
            .IsRequired();
=======
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
            .IsRequired(); 

            entity.Property(x => x.Y)
            .HasColumnName("y_coordinate")
            .IsRequired(); 
<<<<<<< HEAD
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2

            entity.Property(x => x.LastModified)
                .HasColumnName("last_modified")
                .IsRequired();
        });

<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
        base.OnModelCreating(modelBuilder);
    }
}
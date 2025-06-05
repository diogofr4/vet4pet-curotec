using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Vet4PetDbContext : DbContext
    {
        public Vet4PetDbContext(DbContextOptions<Vet4PetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Animals)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Appointments)
                .WithOne(a => a.Vet)
                .HasForeignKey(a => a.VetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Animal
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Appointments)
                .WithOne(ap => ap.Animal)
                .HasForeignKey(ap => ap.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Vet)
                .WithMany()
                .HasForeignKey(a => a.VetId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Owner)
                .WithMany()
                .HasForeignKey(ap => ap.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Required fields
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<Animal>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Animal>().Property(a => a.Species).IsRequired();
            modelBuilder.Entity<Appointment>().Property(ap => ap.Date).IsRequired();
            modelBuilder.Entity<Message>().Property(m => m.Content).IsRequired();
        }
    }
} 
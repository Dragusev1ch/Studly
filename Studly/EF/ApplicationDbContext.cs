using Microsoft.EntityFrameworkCore;
using Studly.DAL.Entities;
using Studly.Entities;

namespace Studly.DAL.EF;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Challenge> Challenges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration for Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(20).IsUnicode(); //emails can have Unicode characters
            entity.Property(e => e.Password).IsRequired().HasMaxLength(64);
            entity.Property(e => e.RegistrationDate).IsRequired().HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Description);
            entity.Property(e => e.Deadline).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Priority).IsRequired();
            entity.Property(e => e.ParentChallengeId).IsRequired(false);
        });

        // Challenge: Self-referencing one-to-many relationship
        modelBuilder.Entity<Challenge>()
            .HasOne(c => c.ParentChallenge)
            .WithMany(c => c.SubTasks)
            .HasForeignKey(c => c.ParentChallengeId)
            .OnDelete(DeleteBehavior.Cascade); // Handling Cascade Delete

        // Challenge-Customer: Many-to-one relationship
        modelBuilder.Entity<Challenge>()
            .HasOne(c => c.Customer)
            .WithMany(c => c.Tasks)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ensuring email is unique and indexed
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Enum conversions 
        modelBuilder.Entity<Challenge>()
            .Property(e => e.Priority)
            .HasConversion<string>();

        modelBuilder.Entity<Challenge>()
            .Property(e => e.Status)
            .HasConversion<string>();

    }
}
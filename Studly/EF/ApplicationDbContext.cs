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

        // Налаштування для Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(20).IsUnicode();
            entity.Property(e => e.Password).IsRequired().HasMaxLength(64);
            entity.Property(e => e.RegistrationDate).IsRequired().HasDefaultValueSql("GETDATE()");
            entity.HasIndex(c => c.Email).IsUnique();
        });

        // Налаштування для Challenge
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.Property(e => e.Title).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Description);
            entity.Property(e => e.Deadline).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.Priority).IsRequired();
            entity.Property(e => e.ParentChallengeId).IsRequired(false);

            // Enum conversions
            entity.Property(e => e.Priority).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();

            // Self-referencing one-to-many relationship for Challenge
            entity.HasOne(c => c.ParentChallenge)
                .WithMany(c => c.SubTasks)
                .HasForeignKey(c => c.ParentChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Challenge-Customer many-to-one relationship
            entity.HasOne(c => c.Customer)
                .WithMany(c => c.Tasks)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
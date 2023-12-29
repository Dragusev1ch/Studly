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
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("GETDATE()");

            // One-to-Many relationship with Challenge
            entity.HasMany(c => c.Tasks)
                .WithOne()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure cascading behavior is desired
        });

        // Configuration for Challenge
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Deadline).IsRequired(); 
            entity.Property(e => e.Priority).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            // Hierarchical structure for SubTasks
            entity.HasMany(e => e.SubTasks)
                .WithOne(d => d.ParentChallenge)
                .HasForeignKey(d => d.ParentChallengeId)
                .OnDelete(DeleteBehavior.Restrict); // Choose DeleteBehavior as per your requirement

            // Making ParentChallengeId nullable for top-level challenges
            entity.Property(e => e.ParentChallengeId).IsRequired(false);
        });
    }
}
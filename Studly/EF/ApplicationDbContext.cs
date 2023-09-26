using Microsoft.EntityFrameworkCore;
using Studly.Entities;

namespace Studly;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<TaskLabel> TaskLabels { get; set; }
    public DbSet<Clock> Clocks { get; set; }
}
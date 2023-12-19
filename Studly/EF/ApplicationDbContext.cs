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
}
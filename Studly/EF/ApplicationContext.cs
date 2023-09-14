using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Studly.Entities;

namespace Studly
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }
        public DbSet<Clock> Clocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
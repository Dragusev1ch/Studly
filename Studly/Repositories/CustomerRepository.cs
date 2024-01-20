using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Studly.DAL.EF;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationDbContext db;
        public CustomerRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public IQueryable<Customer> GetAll()
        {
            return db.Customers.AsQueryable();
        }

        public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
        {
            return db.Customers.Where(predicate).ToList();
        }

        public Customer Create(Customer item)
        {
            return db.Customers.Add(item).Entity;
        }

        public void Update(Customer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var customer = db.Customers.Find(id);
            
            if(customer != null) db.Customers.Remove(customer);
        }
    }
}

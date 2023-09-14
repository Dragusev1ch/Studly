using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationContext db;

        public CustomerRepository(ApplicationContext context)
        {
            db = context;
        }
        public IEnumerable<Customer> GetAll()
        {
            return db.Customers;
        }

        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> Find(Func<Customer, bool> predicate)
        {
            return db.Customers.Where(predicate).ToList();
        }

        public void Create(Customer item)
        {
            db.Customers.Add(item);
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

using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext db;
    private  CustomerRepository<Customer> customerRepository;
    
    private bool _disposed = false;

    public EFUnitOfWork(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    public IRepository<Customer> Customers
    {
        get
        {
            if (customerRepository == null) customerRepository = new CustomerRepository<Customer>(db);

            return customerRepository;
        }
    }

    /*TODO Another Entities*/


    

    public IRepository<Challenge> Challenges { get; }
    public IRepository<Clock> Clocks { get; }
    public IRepository<Comment> Comments { get; }
    public IRepository<Label> Labels { get; }
    public IRepository<TaskLabel> TaskLabels { get; }

    public virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true); 
        GC.SuppressFinalize(this);
    }

    public void Save()
    {
        db.SaveChanges();
    }
}
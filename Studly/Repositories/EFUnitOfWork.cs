using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext db;
    private  CustomerRepository customerRepository;
    
    private bool disposed = false;

    public EFUnitOfWork(ApplicationContext dbContext)
    {
        db = dbContext;
    }

    public IRepository<Customer> Customers
    {
        get
        {
            if (customerRepository == null) customerRepository = new CustomerRepository(db);

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
        if (!disposed)
        {
            if (disposing)
            {
                db.Dispose();
            }
            disposed = true;
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
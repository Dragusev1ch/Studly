using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext db;
    private  CustomerRepository customerRepository;
    private ChallengeRepository challengeRepository;
    
    private bool disposed = false;

    public EFUnitOfWork(ApplicationDbContext dbContext)
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

    public IRepository<Challenge> Challenges
    {
        get
        {
            if (challengeRepository == null) challengeRepository = new ChallengeRepository(db);

            return challengeRepository;
        }
    }
    /*TODO Another Entities*/


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
using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext db;
    private  CustomerRepository _customerRepository;
    private ChallengeRepository _challengeRepository;
    
    private bool _disposed;

    public EFUnitOfWork(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    public IRepository<Customer> Customers
    {
        get
        {
            if (_customerRepository == null) _customerRepository = new CustomerRepository(db);

            return _customerRepository;
        }
    }

    public IRepository<Challenge> Challenges
    {
        get
        {
            if(_challengeRepository == null) _challengeRepository = new ChallengeRepository(db);

            return _challengeRepository;
        }
    }

    /*TODO Another Entities*/


    public virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            db.Dispose();
        }
        _disposed = true;
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
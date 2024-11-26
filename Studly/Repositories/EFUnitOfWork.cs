using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext db;
    private  GenericRepository<Customer> _customerRepository;
    private GenericRepository<Challenge> _challengeRepository;
    private GenericRepository<TimeTrackingSession> _timeTrackingSessionRepository;

    
    private bool _disposed;

    public EFUnitOfWork(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    public IRepository<Customer> Customers
    {
        get
        {
            if (_customerRepository == null) _customerRepository = new GenericRepository<Customer>(db);

            return _customerRepository;
        }
    }

    public IRepository<Challenge> Challenges
    {
        get
        {
            if(_challengeRepository == null) _challengeRepository = new GenericRepository<Challenge>(db);

            return _challengeRepository;
        }
    }

    public IRepository<TimeTrackingSession> TimeTrackingSessions
    {
        get
        {
            if (_timeTrackingSessionRepository == null) _timeTrackingSessionRepository = new GenericRepository<TimeTrackingSession>(db);

            return _timeTrackingSessionRepository;
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
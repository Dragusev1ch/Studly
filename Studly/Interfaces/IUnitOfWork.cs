using Studly.Entities;

namespace Studly.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Challenge> Challenges { get; }
    IRepository<Clock> Clocks { get; }
    IRepository<Comment> Comments { get; }
    IRepository<Customer> Customers { get; }
    IRepository<Label> Labels { get; }
    IRepository<TaskLabel> TaskLabels { get; }
    void Save();
}
using Studly.DAL.Entities;
using Studly.Entities;

namespace Studly.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    Task<IQueryable<T>> Find(Func<T,Boolean> predicate);
    Task<T> Create(T item);
    Task Update(T item);
    Task Delete(int id);
}
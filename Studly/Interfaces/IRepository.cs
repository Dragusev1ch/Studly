using Studly.DAL.Entities;
using Studly.Entities;

namespace Studly.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IEnumerable<T> Find(Func<T,Boolean> predicate);
    T Create(T item);
    void Update(T item);
    void Delete(int id);
}
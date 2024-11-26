using Microsoft.EntityFrameworkCore;
using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IQueryable<T>> Find(Func<T, bool> predicate)
    {
        return await Task.Run(() => _dbSet.Where(predicate).AsQueryable());
    }

    public async Task<T> Create(T item)
    {
        _dbSet.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task Update(T item)
    {
        _dbSet.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var item = await _dbSet.FindAsync(id);

        if (item != null)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
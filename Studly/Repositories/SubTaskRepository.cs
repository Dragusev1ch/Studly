using Microsoft.EntityFrameworkCore;
using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class SubTaskRepository : IRepository<SubTask>
{
    private readonly ApplicationDbContext _context;

    public SubTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<SubTask> GetAll()
    {
        return _context.SubTasks.AsQueryable();
    }

    public SubTask Get(int id)
    {
        return _context.SubTasks.Find(id);
    }

    public IEnumerable<SubTask> Find(Func<SubTask, bool> predicate)
    {
        return _context.SubTasks.Where(predicate).ToList();
    }

    public void Create(SubTask item)
    { 
        _context.SubTasks.Add(item);
    }

    public void Update(SubTask item)
    {
        _context.Entry(item).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var item = _context.SubTasks.Find(id);
        
        if(item != null) _context.SubTasks.Remove(item);
    }
}
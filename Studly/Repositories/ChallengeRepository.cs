using Microsoft.EntityFrameworkCore;
using Studly.DAL.EF;
using Studly.DAL.Entities;
using Studly.Entities;
using Studly.Interfaces;

namespace Studly.Repositories;

public class ChallengeRepository : IRepository<Challenge>
{
    private readonly ApplicationDbContext db;

    public ChallengeRepository(ApplicationDbContext db)
    {
        this.db = db;
    }


    public IQueryable<Challenge> GetAll()
    {
        return db.Challenges.AsQueryable();
    }

    public Challenge Get(int id)
    {
        return db.Challenges.Find(id);
    }

    public IEnumerable<Challenge> Find(Func<Challenge, bool> predicate)
    {
        return db.Challenges.Where(predicate).ToList();
    }

    public void Create(Challenge item)
    {
        db.Challenges.Add(item);
    }

    public void Update(Challenge item)
    {
        db.Entry(item).State = EntityState.Modified;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        var challenge = db.Challenges.Find(id);

        if (challenge != null) db.Challenges.Remove(challenge);
    }
}
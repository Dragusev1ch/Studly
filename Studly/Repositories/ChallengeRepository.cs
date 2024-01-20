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

    public IEnumerable<Challenge> Find(Func<Challenge, bool> predicate)
    {
        return db.Challenges.Where(predicate).ToList();
    }

    public Challenge Create(Challenge item)
    {
        return db.Challenges.Add(item).Entity;
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
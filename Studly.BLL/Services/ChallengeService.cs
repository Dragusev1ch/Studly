using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Studly.BLL.DTO;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.DAL.Entities;
using Studly.DAL.Enums;
using Studly.Interfaces;

namespace Studly.BLL.Services;

public class ChallengeService : IChallengeService
{
    private readonly NullDataException _challengeNotFound = new("Challenge not found",
        "Challenge does not found. It might be deleted or wrong id!");

    private readonly NullDataException _customerNotFound = new("Parent challenge not found",
        "Parent challenge does not found. It might be deleted!");
    
    private readonly LoginException _userDoNotHaveThisChallenge = new("This challenge does not belong to user!",
        "This is might be site error!");

    private readonly IUnitOfWork _database;
    private readonly IMapper _mapper;


    public ChallengeService(IUnitOfWork uof, IMapper mapper)
    {
        _mapper = mapper;
        _database = uof;
    }


    public ChallengeDto Create(ChallengeRegistrationDto challengeDto, string userEmail)
    {
        var challenge = _mapper.Map<Challenge>(challengeDto);

        // Link customer
        var customer = _database.Customers.GetAll().FirstOrDefault(customer => customer.Email == userEmail) ??
                       throw _customerNotFound;

        challenge.CustomerId = customer.Id;
        challenge.Customer = customer;

        // Link parent
        if (challengeDto.ParentChallengeId != null)
        {
            challenge.ParentChallengeId = challengeDto.ParentChallengeId;
            challenge.ParentChallenge = GetChallengeById(challengeDto.ParentChallengeId!.Value);
        }

        // Null sub tasks to create a clear task and after separately add sub tasks
        challenge.SubTasks.Clear();

        // Add Challenge to DB
        var entity = _database.Challenges.Create(challenge);

        _database.Save();

        // Link to already created task list of sub tasks

        if (challengeDto.Subtasks != null) CreateMany(challengeDto.Subtasks, userEmail, entity.Id);

        return GetById(entity.Id);
    }

    public IEnumerable<ChallengeDto> GetUserList(string userEmail)
    {
        var customer = _database.Customers.GetAll().FirstOrDefault(customer => customer.Email == userEmail) ??
                       throw _customerNotFound;

        var challenges = _database.Challenges.GetAll()
            .Where(c => c.CustomerId == customer.Id && c.ParentChallengeId == null)
            .Include(c => c.SubTasks)
            .Select(challenge => _mapper.Map<ChallengeDto>(challenge))
            .AsEnumerable();

        return challenges;
    }

    public IEnumerable<ChallengeDto>? GetUserList(string userEmail, int offset, int count, bool? completedVisible,
        bool? sortByPriority,
        DateVariants? date, ChallengeStatus? sortByStatus)
    {
        var customer = _database.Customers.GetAll().FirstOrDefault(customer => customer.Email == userEmail) ??
                       throw _customerNotFound;

        var request = _database.Challenges.GetAll()
            .Where(c => c.CustomerId == customer.Id && c.ParentChallengeId == null);

        if (completedVisible.HasValue && completedVisible.Value)
            request = request.Where(c => c.Status != ChallengeStatus.Completed);
        if (date.HasValue)
            request = date switch
            {
                DateVariants.Today => request.Where(c => c.Deadline != null && c.Deadline.Value.Date == DateTime.Today),
                DateVariants.Tomorrow => request.Where(c =>
                    c.Deadline != null && c.Deadline.Value.Date == DateTime.Today.AddDays(1)),
                DateVariants.Month => request.Where(c =>
                    c.Deadline != null && c.Deadline.Value.Date < DateTime.Today.AddDays(32)),
                _ => request
            };
        if (sortByStatus.HasValue)
            request = request.Where(c => c.Status == sortByStatus);
        if (sortByPriority.HasValue && sortByPriority.Value)
            request = request.OrderByDescending(c => c.Priority);

        var list = request
            .Skip(offset)
            .Take(count)
            .Include(c => c.SubTasks)
            .Select(challenge => _mapper.Map<ChallengeDto>(challenge))
            .AsEnumerable();


        return list;
    }

    public ChallengeDto GetById(int id)
    {
        return _mapper.Map<ChallengeDto>(GetChallengeById(id));
    }

    public IEnumerable<ChallengeDto> GetList()
    {
        return _database.Challenges.GetAll()
            .Where(c => c.ParentChallengeId == null)
            .Include(c => c.SubTasks)
            .Select(c => _mapper.Map<ChallengeDto>(c))
            .AsEnumerable();
    }

    public ChallengeDto Update(ChallengeUpdateDto newChallenge, int id)
    {
        var entity = GetChallengeById(id);

        if (!string.IsNullOrEmpty(newChallenge.Title)) entity.Title = newChallenge.Title;
        if (!string.IsNullOrEmpty(newChallenge.Description)) entity.Description = newChallenge.Description;
        if (newChallenge.DeadLine.HasValue) entity.Deadline = newChallenge.DeadLine;
        if (newChallenge.Status.HasValue) entity.Status = newChallenge.Status.Value;
        if (newChallenge.Priority.HasValue) entity.Priority = newChallenge.Priority.Value;

        _database.Challenges.Update(entity);

        _database.Save();

        return GetById(id);
    }

    public bool Delete(int id)
    {
        _database.Challenges.Delete(id);

        _database.Save();

        return !_database.Challenges.GetAll().Any(c => c.Id == id);
    }

    public void Dispose()
    {
        _database.Dispose();
    }

    private void CreateMany(List<ChallengeRegistrationDto> challenges, string userEmail, int? parentId)
    {
        foreach (var challenge in challenges)
        {
            challenge.ParentChallengeId = parentId;
            Create(challenge, userEmail);
        }
    }

    private Challenge GetChallengeById(int id)
    {
        return _database.Challenges.GetAll()
            .Include(c => c.SubTasks)
            .ThenInclude(c => c.SubTasks)
            .SingleOrDefault(c => c.Id == id) ?? throw _challengeNotFound;
    }
}
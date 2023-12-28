using AutoMapper;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.Infrastructure.Exceptions;
using Studly.BLL.Interfaces.Services;
using Studly.DAL.Entities;
using Studly.Interfaces;

namespace Studly.BLL.Services;

public class ChallengeService : IChallengeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _database;

    public ChallengeService(IUnitOfWork uof,IMapper mapper)
    {
        _mapper = mapper;
        _database = uof;
    }

    public void CreateChallenge(ChallengeRegistrationDto challengeDto,string email)
    {
        var challenge = _mapper.Map<Challenge>(challengeDto);

        _database.Challenges.Create(challenge);

        var customer = _database.Customers.GetAll().FirstOrDefault(customer => customer.Email == email);

        if (customer == null)
            throw new NullDataException("Customer not found",
                "Information about you is not exist in database");

        customer.Tasks.Add(challenge);

        _database.Customers.Update(customer);
        _database.Save();
    }

    public ChallengeDto? GetChallenge(string title)
    {
        throw new NotImplementedException();
    }

    public ChallengeDto GetChallengeById(int id)
    {
        throw new NotImplementedException();
    }

    public ChallengeDto GetCurrentChallenge(string title)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ChallengeDto> List()
    {
        return _database.Challenges.GetAll().Select(challenge => _mapper.Map<ChallengeDto>(challenge));
    }

    public ChallengeDto Update(ChallengeUpdateDto newChallenge, string title)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
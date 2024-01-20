﻿using AutoMapper;
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

    public void Create(ChallengeRegistrationDto challengeDto,string email)
    {
        var challenge = _mapper.Map<Challenge>(challengeDto);
        var customer = _database.Customers.GetAll().FirstOrDefault(customer => customer.Email == email);

        if (customer == null)
            throw new NullDataException("Customer not found",
                "Information about you is not exist in database");

        challenge.CustomerId = customer.CustomerId;
        challenge.Customer = customer;

        _database.Challenges.Create(challenge);



        _database.Save();
    }

    private List<Challenge> Find(ChallengeDto challenge)
    {
        return challenge.SubTasks
            .Select(sub => _mapper.Map<Challenge>(sub)).ToList(); 
    }

    private void AddSubtask(List<Challenge> list)
    {
        if(list.Count == 0) return;

        foreach (var item in list)
        {
            _database.Challenges.Create(item);
        }
    }

    public ChallengeDto? Get(string title)
    {
        var challenge = _database.Challenges
            .GetAll()
            .FirstOrDefault(challenge => challenge.Title == title);

        if (challenge == null)
            throw new NullDataException("Challenge not found",
                "Information about challenge with this name is exist");

        return _mapper.Map<ChallengeDto>(challenge);
    }

    public ChallengeDto GetById(int id)
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
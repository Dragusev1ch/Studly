﻿    using Studly.DAL.Entities;
    using Studly.Entities;

namespace Studly.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Challenge> Challenges { get; }
    IRepository<Customer> Customers { get; }
    IRepository<SubTask> SubTasks { get; }
    void Save();
}
﻿using Studly.Entities;

namespace Studly.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<Customer> GetAll();
    T Get(int id);
    IEnumerable<T> Find(Func<T,Boolean> predicate);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}
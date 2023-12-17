﻿using eLibrary.Domain.Entities;
using System.Linq.Expressions;

namespace eLibrary.Domain.Services
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> FindAsync (int id);
        Task<List<T>> GetAllAsync ();
        Task<List<T>> FindWhere (Expression<Func<T, bool>> predicate);
        Task<T> AddAsync (T entity);
        Task<T> UpdateAsync (T entity);
        Task DeleteAsync (T entity);
    }
    
}

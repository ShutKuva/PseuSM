﻿using DAL.Abstractions.Repository;

namespace DAL.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<IRepository<TEntity, TId, TPredicate>> GetRepositoryAsync<TEntity, TId, TPredicate>();
        Task CommitAsync();
    }
}

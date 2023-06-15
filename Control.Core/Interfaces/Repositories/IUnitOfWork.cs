﻿using Control.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Control.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
       
        IGenericRepository<DocumentType> DocumentTypeRepositoryAsync { get; }

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();
    }
}
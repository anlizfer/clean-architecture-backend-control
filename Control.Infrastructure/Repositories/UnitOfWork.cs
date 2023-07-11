using Control.Core.Interfaces.Repositories;
using Control.Domain.Entities;
using Control.Infrastructure.Repositories.RespositoryAsync;
using Control.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Control.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ControlContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ControlContext context)
        {
            _context = context;
        }        


        public IGenericRepository<DocumentType> DocumentTypeRepositoryAsync => new DocumentTypeRepositoryAsync(_context);
        public IGenericRepository<Countries> CountriesRepositoryAsync => new CountriesRepository(_context);
        public IGenericRepository<States> StatesRepositoryAsync => new StatesRepository(_context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
        }

        public async Task CommitAsync()
        {
            try
            {
                await BeginTransactionAsync();
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                Dispose();
            }
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (!disposing)
                {
                }
                else
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task RollbackAsync()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            return Task.CompletedTask;
        }
    }
}
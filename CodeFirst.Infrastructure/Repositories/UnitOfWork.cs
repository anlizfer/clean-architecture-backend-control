using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Domain.Entities;
using CodeFirst.Infrastructure.Repositories.RespositoryAsync;
using CodeFirst.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodeFirstContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(CodeFirstContext context)
        {
            _context = context;
        }

        public IGenericRepository<Student> StudentRepositoryAsync => new StudentRepositoryAsync(_context);

        public IGenericRepository<Course> CourseRepositoryAsync => new CourseRepositoryAsync(_context);

        public IGenericRepository<Inscription> InscriptionRepositoryAsync => new InscriptionRepositoryAsync(_context);

        public IGenericRepository<IdentityUser> UserRepositoryAsync => new UserRepositoryAsync(_context);

        public IGenericRepository<IdentityRole> RoleRepositoryAsync => new RoleRepositoryAsync(_context);

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
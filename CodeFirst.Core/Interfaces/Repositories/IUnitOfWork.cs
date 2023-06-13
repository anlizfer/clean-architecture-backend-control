using CodeFirst.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> StudentRepositoryAsync { get; }
        IGenericRepository<Course> CourseRepositoryAsync { get; }
        IGenericRepository<Inscription> InscriptionRepositoryAsync { get; }
        IGenericRepository<IdentityUser> UserRepositoryAsync { get; }
        IGenericRepository<IdentityRole> RoleRepositoryAsync { get; }
        IGenericRepository<DocumentType> DocumentTypeRepositoryAsync { get; }

        Task BeginTransactionAsync();

        Task CommitAsync();

        Task RollbackAsync();
    }
}
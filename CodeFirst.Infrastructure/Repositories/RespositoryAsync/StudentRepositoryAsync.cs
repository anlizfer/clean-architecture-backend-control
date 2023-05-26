using CodeFirst.Domain.Entities;
using CodeFirst.Infrastructure.Settings;

namespace CodeFirst.Infrastructure.Repositories.RespositoryAsync
{
    public class StudentRepositoryAsync : GenericRepository<Student>
    {
        public StudentRepositoryAsync(CodeFirstContext codeFirstContext) : base(codeFirstContext)
        {
        }
    }
}
using CodeFirst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultCourses
    {
        public static async Task SeedDefaultCoursesAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    Id = 1,
                    Name = "Matematicas",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                },
                new Course()
                {
                    Id = 2,
                    Name = "Español",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                },
                new Course()
                {
                    Id = 3,
                    Name = "Química",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                },
                new Course()
                {
                    Id = 4,
                    Name = "Ingles",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                },
                new Course()
                {
                    Id = 5,
                    Name = "Fisica",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                },
                new Course()
                {
                    Id = 6,
                    Name = "Religión",
                    StateId = Domain.Enums.Estado.Active,
                    CreatedBy = "Angel Fernando Lizcano",
                }

            );
            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
using CodeFirst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultInscriptions
    {
        public static async Task SeedDefaultInscriptionsAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inscription>().HasData(
                new Inscription()
                {
                    Id = 1,
                    CourseId = 1,
                    StudentId = 1
                },
                new Inscription()
                {
                    Id = 2,
                    CourseId = 2,
                    StudentId = 1
                },
                new Inscription()
                {
                    Id = 3,
                    CourseId = 4,
                    StudentId = 2
                },
                new Inscription()
                {
                    Id = 4,
                    CourseId = 2,
                    StudentId = 3
                },
                new Inscription()
                {
                    Id = 5,
                    CourseId = 4,
                    StudentId = 4
                }

            );

            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
using CodeFirst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultStudents
    {
        public static async Task SeedDefaultStudentsAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    Name = "Angel 01",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 2,
                    Name = "Angel 02",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 3,
                    Name = "Angel 03",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 4,
                    Name = "Angel 04",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 5,
                    Name = "Angel 05",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 6,
                    Name = "Angel 06",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                },
                new Student()
                {
                    Id = 7,
                    Name = "Angel 07",
                    DateOfBirth = new DateTime(2000, 1, 1, 7, 0, 0),
                }

            );
            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
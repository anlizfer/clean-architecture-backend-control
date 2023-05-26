using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedDefaultUsersAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().HasData(
                    new IdentityUser
                    {
                        Id = "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                        UserName = "angel.lizcano.sie@gmail.com",
                        NormalizedUserName = "angel.lizcano.sie@gmail.com",
                        Email = "angel.lizcano.sie@gmail.com",
                        NormalizedEmail = "angel.lizcano.sie@gmail.com",
                        PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Pixel@02A")
                    }
                );
            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}

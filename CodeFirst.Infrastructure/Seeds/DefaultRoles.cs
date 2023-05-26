using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedDefaultRolesAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole
                    {
                        Id = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d",
                        Name = "Admin",
                        NormalizedName = "Admin"
                    }
                );
            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}

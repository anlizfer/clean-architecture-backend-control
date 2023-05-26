using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class ModelBuilderSeed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //#region Users
            //Task.Run(() => DefaultRoles.SeedDefaultRolesAsync(modelBuilder).ConfigureAwait(false));
            //Task.Run(() => DefaultUsers.SeedDefaultUsersAsync(modelBuilder).ConfigureAwait(false));
            //Task.Run(() => DefaultUserClaim.SeedDefaultUserClaimAsync(modelBuilder).ConfigureAwait(false));
            //#endregion
            Task.Run(() => DefaultStudents.SeedDefaultStudentsAsync(modelBuilder).ConfigureAwait(false));
            Task.Run(() => DefaultCourses.SeedDefaultCoursesAsync(modelBuilder).ConfigureAwait(false));
            Task.Run(() => DefaultInscriptions.SeedDefaultInscriptionsAsync(modelBuilder).ConfigureAwait(false));
        }
    }
}
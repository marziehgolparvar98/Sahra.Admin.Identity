using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sahra.DataLayer.Models.Identity;
using System.Linq;
namespace Sahra.DataLayer.Models.DbContext
{
    public static class DataSeeder
    {
        public static void AccelaratorSeedData(this IApplicationBuilder appBuilder)
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            };
        }
        public static void ESahraSeedData(this IApplicationBuilder appBuilder)
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ESahraApplicationDbContext>();
                dbContext.Database.Migrate();
            };
        }
    }
}

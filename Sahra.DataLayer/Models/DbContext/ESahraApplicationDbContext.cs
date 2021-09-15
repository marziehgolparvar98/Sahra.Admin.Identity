using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sahra.DataLayer.Models.Entities;
using Sahra.DataLayer.Models.Identity;

namespace Sahra.DataLayer.Models.DbContext
{
    public class ESahraApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ESahraApplicationDbContext(DbContextOptions<ESahraApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<ESahraProduct> ESahraProducts { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ESahraUploadFile> ESahraUploadFile { get; set; }
        public DbSet<Solution> Solutions { get; set; }
    }
}

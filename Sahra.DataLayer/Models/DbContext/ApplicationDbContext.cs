using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sahra.DataLayer.Models.Entities;
using Sahra.DataLayer.Models.Identity;

namespace Sahra.DataLayer.Models.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CategoryInvestReq> CategoryInvestReqs { get; set; }
        public DbSet<Collabration> Collabrations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<InvestRequest> InvestRequests { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<RelationOfInvestReq> RelationOfInvestReqs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.Services.Service;
using Serilog;
using System.Text;

namespace Sahra.Accelerator.api.Startups.Service
{
    public static class Services
    {
        public static void AddServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddCors();
           // service.AddSingleton(Log.Logger);
            service.AddScoped<IInvestRequestService, InvestRequestService>();
            service.AddScoped<INewsService, NewsService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<INewsCategoryService, NewsCategoryService>();
            service.AddScoped<IEventService, EventService>();
            service.AddScoped<ISliderService, SliderService>();
            service.AddScoped<ISliderImageService, SliderImageService>();
            //service.AddScoped<ILoginService, LoginService>();
        }

        //public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        //{
        //    #region Identity
            // For Identity  
            //services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            //{
            //    option.User.RequireUniqueEmail = true;
            //    option.Password.RequiredLength = 8;
            //    option.Password.RequireDigit = true;
            //    option.Password.RequireLowercase = true;
            //    option.Password.RequireUppercase = true;
            //    option.Password.RequireNonAlphanumeric = true;

            //})
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //// Adding Authentication  
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})

            // Adding Jwt Bearer  
            //.AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidAudience = configuration["JWT:ValidAudience"],
            //        ValidIssuer = configuration["JWT:ValidIssuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            //    };
            //});
        //    #endregion Identity
        //}
    }
}
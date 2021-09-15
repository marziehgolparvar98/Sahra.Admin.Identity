using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Repository;

namespace Sahra.Accelerator.api.Startups.Repository
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IInvestRequestRepository, InvestRequestRepository>();
            service.AddScoped<IEventRepository, EventRepository>();
            service.AddScoped<INewsRepository, NewsRepository>();
            service.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
            service.AddScoped<ISliderRepository, SliderRepository>();
            service.AddScoped<ISliderImageRepository, SliderImageRepository>();
            service.AddScoped<IEmailRepository, EmailRepository>();
        }
    }
}
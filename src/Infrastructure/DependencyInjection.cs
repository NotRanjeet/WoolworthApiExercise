using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Infrastructure.Api;
using Woolworth.Infrastructure.Services;

namespace Woolworth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            // Can be injected as Singleton as it just read from config
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddHttpClient<IWoolworthApi,WoolworthApi>(c =>
            {
                c.BaseAddress = new System.Uri(configuration.GetValue<string>("ApiBaseUrl"));
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("User-Agent", "WoolworthTests");
            });
            return services;
        }
    }
}
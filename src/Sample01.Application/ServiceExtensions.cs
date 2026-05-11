using Microsoft.Extensions.DependencyInjection;
using Sample01.Application.Interfaces;
using Sample01.Application.Services;

namespace Sample01.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample01.Application.Core.Services;
using Sample01.Domain.Core.Repositories;
using Sample01.Infrastructure.Data;
using Sample01.Infrastructure.Repositories;
using Sample01.Infrastructure.Services;

namespace Sample01.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<Sample01DbContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:Sample01Database",
                x => x.MigrationsAssembly("Sample01.Infrastructure")));

            services.AddScoped(typeof(IBaseRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<Sample01DbContext>>();

            using (var dbContext = new Sample01DbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
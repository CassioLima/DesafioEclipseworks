using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application;
using Domain;
using System.Net.Security;
using Infra;

namespace API
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager config)
        {
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(ICommandResult).Assembly); });

            services.AddScoped<INotificationContext, NotificationContext>();

            services.AddDbContextPool<DbContext2>(options => options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("ApiDb"));

            services.AddScoped<IUnitOfWork>(provider => provider.GetService<DbContext2>());

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            return services;
        }
    }
}

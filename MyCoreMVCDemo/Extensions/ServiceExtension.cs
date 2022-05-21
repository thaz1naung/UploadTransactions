using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using MyCoreMVCDemo.Repositories;
using MyCoreMVCDemo.Repositories.Interfaces;
using MyCoreMVCDemo.Services;
using MyCoreMVCDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {

            var connectionString =config["ConnectionStrings:DBConnection"];
            services.AddDbContext<MyCoreDbContext>(o => o.UseSqlServer(connectionString,
                sqlServerOptionsAction : sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }));

        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IImportService, ImportService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IImportRepository, ImportRepository>();
        }
        public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DBSetting>(config.GetSection("ConnectionStrings"));
            services.AddSingleton<IDBSetting>(x => x.GetRequiredService<IOptions<DBSetting>>().Value);
        }
    }
   
}

using ePizzaHub.Entities;
using ePizzaHub.Repositories;
using ePizzaHub.Repositories.Implementations;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Configuration
{
    public class ConfigureRepositories
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            services.AddIdentity<User, Role>().
               AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScoped<DbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });
           
           
        }

        public static void AddServices(object services, object configuration)
        {
            throw new NotImplementedException();
        }
    }
}

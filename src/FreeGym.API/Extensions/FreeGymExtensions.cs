using FreeGym.Core.Interfaces.Repositories;
using FreeGym.Core.Interfaces.UnitOfWork;
using FreeGym.Core.Services;
using FreeGym.Data.Context;
using FreeGym.Data.Repositories;
using FreeGym.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeGym.API.Extensions
{
    public static class FreeGymExtensions
    {
        public static void AddFreeGymServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FreeGymContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("FreeGymConnection"));
            });

            services.AddTransient<IMusclesRepository, MusclesRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<MusclesService>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeGym.Data.Context;
using FreeGym.Data.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FreeGym.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<FreeGymContext>();
                    await context.Database.MigrateAsync();

                    var identityContext = services.GetRequiredService<IdentityDbContext>();
                    await identityContext.Database.MigrateAsync();
                    
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    await IdentitySeed.SeedUserAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Ocorreu um erro ao aplicar migrações ou aplicar seed de dados");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

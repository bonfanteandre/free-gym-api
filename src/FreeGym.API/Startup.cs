using FreeGym.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using FreeGym.Data.Repositories;
using FreeGym.Core.Interfaces.Repositories;
using FreeGym.Core.Interfaces.UnitOfWork;
using FreeGym.Data.UnitOfWork;
using FreeGym.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using FreeGym.API.Middlewares;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using FreeGym.Core.Entities;
using System.Text;
using FreeGym.Data.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FreeGym.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FreeGymContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("FreeGymConnection"));
            });

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("FreeGymConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            var jwtOptionsSection = _configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(jwtOptionsSection);

            var jwtOptions = jwtOptionsSection.Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<TokenService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IMusclesRepository, MusclesRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<MusclesService>();

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("free-gym-api-version");
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeGym.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeGym.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

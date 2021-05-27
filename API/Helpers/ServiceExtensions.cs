using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
namespace API.Helpers
{
    public static class ServiceExtensions
    {
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StoreContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
        }
        public static void ConfigureValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                  var errors = actionContext.ModelState
                  .Where(e => e.Value.Errors.Count>0)
                  .SelectMany(x => x.Value.Errors)
                  .Select(x => x.ErrorMessage).ToArray();

                  var errorResponse = new ApiValidationErrorResponse
                  {
                      Errors = errors
                  };
                  return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors( opt => {
                opt.AddPolicy("CorsPolicy",policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }
    }
}
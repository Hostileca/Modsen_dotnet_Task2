using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Implementations;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLogicLayer
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AutoMapperConfigure()
                .RepositoriesConfigure()
                .ServicesConfigure()
                .DbConfigure(configuration)
                .ValidationConfigure();

            return services;
        }

        private static IServiceCollection RepositoriesConfigure(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            return services;
        }

        private static IServiceCollection ServicesConfigure(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            return services;
        }

        private static IServiceCollection DbConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnectionBuilder = new SqlConnectionStringBuilder();
            sqlConnectionBuilder.ConnectionString = configuration.GetConnectionString("SQLDbConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(sqlConnectionBuilder.ConnectionString));
            return services;
        }

        private static IServiceCollection AutoMapperConfigure(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        private static IServiceCollection ValidationConfigure(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }

        public static IServiceProvider StartApplication(this IServiceProvider services)
        {
            services
                .AddRoles();
            return services;
        }

        private static IServiceProvider AddRoles(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    if (!context.Roles.Any())
                    {
                        context.Roles.AddRange(
                            new Role { Id = Guid.NewGuid(), Name = RoleConstants.Admin },
                            new Role { Id = Guid.NewGuid(), Name = RoleConstants.User }
                        );

                        context.SaveChanges();
                    }
                }
            }
            return services;
        }
    }
}

using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Implementations;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services, IConfiguration configuration)
        {
            services
                .DbConfigure(configuration)
                .AutoMapperConfigure()
                .RepositoriesConfigure()
                .ServicesConfigure();

            return services;
        }

        private static IServiceCollection RepositoriesConfigure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
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
    }
}

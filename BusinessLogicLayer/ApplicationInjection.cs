using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.Profiles;

namespace BusinessLogicLayer
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ServicesConfigure()
                .DbConfigure(configuration)
                .AutoMapperConfigure();

            return services;
        }

        private static IServiceCollection ServicesConfigure(this IServiceCollection services)
        {

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
            services.AddAutoMapper(builder =>
            {
                builder.AddProfile(typeof(CategoryMappingProfile));
                builder.AddProfile(typeof(OrderItemMappingProfile));
                builder.AddProfile(typeof(OrderItemMappingProfile));
                builder.AddProfile(typeof(ProductMappingProfile));
                builder.AddProfile(typeof(RoleMappingProfile));
                builder.AddProfile(typeof(UserMappingProfile));
            });
            return services;
        }
    }
}

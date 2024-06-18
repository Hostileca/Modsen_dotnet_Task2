using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ServicesConfigure()
                .DbConfigure(configuration);

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
    }
}

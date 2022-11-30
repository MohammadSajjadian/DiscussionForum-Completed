using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extension.DBConnection
{
    public static class SqlConnection
    {
        public static IServiceCollection ConnectToSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBforum>(x => x.UseSqlServer(configuration.GetConnectionString("DBforum")));
            return services;
        }
    }
}

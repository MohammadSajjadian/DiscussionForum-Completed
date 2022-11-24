using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DBConnection
{
    public static class SqlConnection
    {
        public static IServiceCollection ConnectToSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBforum>(x =>
                x.UseSqlServer(configuration.GetConnectionString("DBforum"))
            );
            
            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Extensions
{
    public static class RedisExtensions
    {
        private const string ConnectionString = "RedisCache";
        public static IServiceCollection AddRedisConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(
                 options => options.Configuration = configuration.GetConnectionString(ConnectionString));

            return services;
        }

        public static IHealthChecksBuilder AddRedisHealth(this IHealthChecksBuilder services,
           IConfiguration configuration)
        {
            //services.AddRedis(configuration.GetConnectionString(ConnectionString));

            return services;
        }
    }
}

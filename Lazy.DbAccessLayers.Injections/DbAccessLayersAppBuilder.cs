using Lazy.DbAccessLayers.Core;
using Lazy.DbAccessLayers.Core.AbstractCentralizedFactory;
using Lazy.DbAccessLayers.Core.AbstractCentralizedFactory.Factories;
using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.DataBaseContext.ServicesProvider;
using Lazy.DbAccessLayers.Core.Services.Concretes;
using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Mapping;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using Lazy.DbAccessLayers.Core.Services.Regexes.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Lazy.DbAccessLayers.Injections
{
    public static class DbAccessLayersAppBuilder
    {
        public static IServiceCollection AddDbAccessLayer(this IServiceCollection services, DataBaseParameters dbParams)
        {
            services.AddSingleton(dbParams);
            services.AddScoped<IDbPropertiesProvider, DbPropertiesProvider>();

            switch (dbParams.FactoryType)
            {
                case FactoryType.Sql:
                    services.AddScoped<IFactory<DbProviderFactory>, SqlFactory>();
                    break;

                default:
                    throw new ArgumentNullException("Any DbProviderFactory provided");
            }

            switch (dbParams.RegexType)
            {
                case RegexType.camel_to_snake:
                    services.AddScoped<IRegexFormatter, SnakeCaseFormatter>();
                    break;

                case RegexType.None:
                default:
                    break;
            }

            services.AddScoped<IDbConnectionProvider, DbConnectionProvider>();

            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<ICreator, Creator>();
            services.AddScoped<IMultiReader, MultiReader>();
            services.AddScoped<ISingleReader, SingleReader>();
            services.AddScoped<IUpdator, Updator>();
            services.AddScoped<IDeletor, Deletor>();

            services.AddScoped<IDbServiceProvider, DbServiceProvider>();

            return services;
        }
    }
}

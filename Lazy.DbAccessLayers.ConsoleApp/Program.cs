using Lazy.DbAccessLayers.Core.DataBaseContext.ServicesProvider;
using Lazy.DbAccessLayers.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace Lazy.DbAccessLayers.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection().AddDbAccessLayer(new Core.DataBaseParameters()
                {
                    ConnectionString = "",
                    FactoryType = Core.AbstractCentralizedFactory.FactoryType.Sql,
                    RegexType = Core.Services.Regexes.RegexType.camel_to_snake,
                }).BuildServiceProvider();

                IDbServiceProvider provider = services.GetRequiredService<IDbServiceProvider>();

                Console.WriteLine(provider.Deletor.GetType());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

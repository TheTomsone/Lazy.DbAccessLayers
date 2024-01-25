using System.Data.Common;
using System.Data.SqlClient;

namespace Lazy.DbAccessLayers.Core.AbstractCentralizedFactory.Factories
{
    public class SqlFactory : AbstractFactory<DbProviderFactory>, IFactory<DbProviderFactory>
    {
        public override DbProviderFactory CreateInstance()
        {
            return SqlClientFactory.Instance;
        }
    }
}

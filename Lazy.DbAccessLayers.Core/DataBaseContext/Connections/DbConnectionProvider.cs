using Lazy.DbAccessLayers.Core.AbstractCentralizedFactory;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.Connections
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly DbConnection _connection;

        public DbConnectionProvider(IFactory<DbProviderFactory> factory, DataBaseParameters connParams)
        {
            _connection = factory.CreateInstance().CreateConnection();

            if (_connection == null)
                throw new ArgumentNullException("Something went wrong, while creating a connection to database");

            _connection.ConnectionString = connParams.ConnectionString;
        }

        public DbConnection Connection => _connection;

        public IDbConnectionProvider ChangeConnection(string connectionString)
        {
            _connection.ConnectionString = connectionString;

            return this;
        }
    }
}

using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Concretes
{
    public class Deletor(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IRegexFormatter? regexFormatter = null
        ) : AbstractConnectionService(propertiesProvider, connectionProvider, regexFormatter), IDeletor
    {
        public async Task<int> Async<TModel>(int id, string compositeKey = "") where TModel : class, new()
        {
            return await Task.Run(() => Delete<TModel>(id, compositeKey));
        }
        public int Sync<TModel>(int id, string compositeKey = "") where TModel : class, new()
        {
            return Delete<TModel>(id, compositeKey);
        }

        private int Delete<TModel>(int id, string compositeKey = "") where TModel : class
        {
            using (DbCommand cmd = _connectionProvider.Connection.CreateCommand())
            {
                DbParameter dbParameter = cmd.CreateParameter();
                string keyName = string.IsNullOrEmpty(compositeKey) ? _propertiesProvider.Property<TModel>("Id").Name : _propertiesProvider.Property<TModel>(compositeKey).Name;
                string query = $"DELETE FROM {Tablename<TModel>()} WHERE {PropertyName<TModel>(keyName)} = @{keyName}";

                dbParameter.ParameterName = keyName;
                dbParameter.Value = id;
                cmd.Parameters.Add(dbParameter);
                cmd.CommandText = query;

                Console.WriteLine(query);

                _connectionProvider.Connection.Open();

                try
                {
                    return cmd.ExecuteNonQuery();
                }
                finally
                {
                    _connectionProvider.Connection.Close();
                }
            }
        }
    }
}

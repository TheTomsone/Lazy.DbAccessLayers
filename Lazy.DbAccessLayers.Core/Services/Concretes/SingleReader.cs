using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Mapping;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Concretes
{
    public class SingleReader(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IMapper mapper,
        IRegexFormatter? regexFormatter = null
        ) : AbstractReaderService(propertiesProvider, connectionProvider, mapper, regexFormatter), ISingleReader
    {
        public async Task<TModel> Async<TModel>(int id, string compositeKey = "") where TModel : class, new()
        {
            return await Task.Run(() => Read<TModel>(id, compositeKey));
        }
        public TModel Sync<TModel>(int id, string compositeKey = "") where TModel : class, new()
        {
            return Read<TModel>(id, compositeKey);
        }

        private TModel Read<TModel>(int id, string compositeKey = "") where TModel : class, new()
        {
            using (DbCommand cmd = _connectionProvider.Connection.CreateCommand())
            {
                DbParameter dbParameter = cmd.CreateParameter();
                string query = $"SELECT * FROM {Tablename<TModel>()} WHERE id = @id";

                dbParameter.ParameterName = "id";
                dbParameter.Value = id;
                cmd.Parameters.Add(dbParameter);
                cmd.CommandText = query;

                Console.WriteLine(query);

                _connectionProvider.Connection.Open();

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        if (reader.Read())
                            return _mapper.Map<TModel>(reader);

                        return new TModel();
                    }
                    finally
                    {
                        _connectionProvider.Connection.Close();
                    }
                }

            }
        }
    }
}

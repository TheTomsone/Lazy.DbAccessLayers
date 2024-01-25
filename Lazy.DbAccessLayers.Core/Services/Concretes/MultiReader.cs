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
    public class MultiReader(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IMapper mapper,
        IRegexFormatter? regexFormatter = null
        ) : AbstractReaderService(propertiesProvider, connectionProvider, mapper, regexFormatter), IMultiReader
    {
        public async Task<IEnumerable<TModel>> Async<TModel>() where TModel : class, new()
        {
            return await Task.Run(() => Read<TModel>());
        }
        public IEnumerable<TModel> Sync<TModel>() where TModel : class, new()
        {
            return Read<TModel>();
        }

        private IEnumerable<TModel> Read<TModel>() where TModel : class, new()
        {
            using (DbCommand cmd = _connectionProvider.Connection.CreateCommand())
            {
                string query = $"SELECT * FROM {Tablename<TModel>()}";

                cmd.CommandText = query;
                _connectionProvider.Connection.Open();

                Console.WriteLine(query);

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {

                        if (reader.HasRows)
                            while (reader.Read())
                                yield return _mapper.Map<TModel>(reader);
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

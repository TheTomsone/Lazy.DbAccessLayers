using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Concretes
{
    public class Updator(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IRegexFormatter? regexFormatter = null
        ) : AbstractConnectionService(propertiesProvider, connectionProvider, regexFormatter), IUpdator
    {
        public Task<int> Async<TModel>(TModel model, bool isPartial = true, string compositeKey = "") where TModel : class, new()
        {
            return Task.Run(() => Update(model, isPartial, compositeKey));
        }
        public int Sync<TModel>(TModel model, bool isPartial = true, string compositeKey = "") where TModel : class, new()
        {
            return Update(model, isPartial, compositeKey);
        }

        private int Update<TModel>(TModel model, bool isPartial, string compositeKey) where TModel : class
        {
            using (DbCommand cmd = _connectionProvider.Connection.CreateCommand())
            {
                StringBuilder query = new StringBuilder($"UPDATE {Tablename<TModel>()} SET ");
                PropertyInfo[] props = _propertiesProvider.Properties<TModel>();

                if (string.IsNullOrEmpty(compositeKey))
                    props = props.Where(prop => prop != _propertiesProvider.Property<TModel>("Id")).ToArray();

                props = props.Where(prop => prop.GetValue(model) != null).ToArray();

                for (int i = 0; i < props.Length; i++)
                {
                    object? value = props[i].GetValue(model);

                    if (value == null ||
                        isPartial && props[i].PropertyType == typeof(string) && (string)value == "" ||
                            props[i].PropertyType == typeof(int) && (int)value == 0)
                        continue;

                    DbParameter param = cmd.CreateParameter();

                    param.ParameterName = props[i].Name;
                    param.Value = props[i].GetValue(model);
                    cmd.Parameters.Add(param);

                    if (i > 0)
                        query.Append(",");

                    query.Append($"{PropertyName<TModel>(props[i].Name)} = @{props[i].Name}");
                }

                string keyName = string.IsNullOrEmpty(compositeKey) ? _propertiesProvider.Property<TModel>("Id").Name : compositeKey;
                DbParameter paramId = cmd.CreateParameter();

                paramId.ParameterName = keyName;
                Console.WriteLine(keyName);
                paramId.Value = _propertiesProvider.Property<TModel>(keyName).GetValue(model);
                cmd.Parameters.Add(paramId);

                query.Append($" WHERE {PropertyName<TModel>(keyName)} = @{keyName}");

                Console.WriteLine(query.ToString());

                cmd.CommandText = query.ToString();

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

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
    public class Creator(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IRegexFormatter? regexFormatter = null
        ) : AbstractConnectionService(propertiesProvider, connectionProvider, regexFormatter), ICreator
    {
        public async Task<int> Async<TModel>(TModel model, bool isComposite = false) where TModel : class, new()
        {
            return await Task.Run(() => Create(model, isComposite));
        }
        public int Sync<TModel>(TModel model, bool isComposite = false) where TModel : class, new()
        {
            return Create(model, isComposite);
        }

        private int Create<TModel>(TModel model, bool isComposite) where TModel : class
        {
            using (DbCommand cmd = _connectionProvider.Connection.CreateCommand())
            {
                StringBuilder queryFront = new StringBuilder($"INSERT INTO {Tablename<TModel>()} ");
                StringBuilder queryBack = new StringBuilder(" VALUES ");
                PropertyInfo[] props = _propertiesProvider.Properties<TModel>();

                if (!isComposite)
                    props = props.Where(prop => prop != _propertiesProvider.Property<TModel>("Id")).ToArray();

                props = props.Where(prop => prop.GetValue(model) != null).ToArray();
                queryFront.Append("(");
                queryBack.Append("(");

                for (int i = 0; i < props.Length; i++)
                {
                    object? propValue = props[i].GetValue(model);

                    if (propValue == null || props[i].PropertyType == typeof(string) &&
                                (string)propValue == "" || props[i].PropertyType == typeof(int) && (int)propValue == 0)
                        continue;

                    DbParameter param = cmd.CreateParameter();

                    param.ParameterName = props[i].Name;
                    param.Value = props[i].GetValue(model);
                    cmd.Parameters.Add(param);

                    if (i > 0)
                    {
                        queryFront.Append(",");
                        queryBack.Append(",");
                    }

                    queryFront.Append(PropertyName<TModel>(props[i].Name));
                    queryBack.Append($"@{props[i].Name}");
                }

                queryFront.Append(")");
                queryBack.Append(")");
                cmd.CommandText = queryFront.ToString() + queryBack.ToString();

                Console.WriteLine(cmd.CommandText);

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

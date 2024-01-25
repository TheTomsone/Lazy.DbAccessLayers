using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Mapping
{
    public class Mapper(
        IDbPropertiesProvider propertiesProvider,
        IRegexFormatter? regexFormatter = null
        ) : AbstractService(propertiesProvider, regexFormatter), IMapper
    {
        public TModel Map<TModel>(IDataReader reader) where TModel : class, new()
        {
            TModel model = new TModel();

            foreach (PropertyInfo prop in _propertiesProvider.Properties<TModel>())
            {
                object value = reader[$"{PropertyName<TModel>(prop.Name)}"];

                if (value != null && value != DBNull.Value)
                    prop.SetValue(model, value);
            }

            return model;
        }
    }
}

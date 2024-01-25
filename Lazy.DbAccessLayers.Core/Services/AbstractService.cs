using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services
{
    public abstract class AbstractService(
        IDbPropertiesProvider propertiesProvider,
        IRegexFormatter? regexFormatter = null
        )
    {
        protected readonly IDbPropertiesProvider _propertiesProvider = propertiesProvider;
        protected readonly IRegexFormatter? _regexFormatter = regexFormatter;

        protected string Tablename<TModel>()
        {
            string tablename = _propertiesProvider.Tablename<TModel>();

            if (_regexFormatter == null)
                return tablename;

            return _regexFormatter.Format(tablename);
        }
        protected string PropertyName<TModel>(string propName)
        {
            string prop = _propertiesProvider.Property<TModel>(propName).Name;

            if (_regexFormatter == null)
                return prop;

            return _regexFormatter.Format(prop);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider
{
    public interface IDbPropertiesProvider
    {
        string Tablename<TModel>();
        PropertyInfo[] Properties<TModel>();
        PropertyInfo Property<TModel>(string propName);
    }
}

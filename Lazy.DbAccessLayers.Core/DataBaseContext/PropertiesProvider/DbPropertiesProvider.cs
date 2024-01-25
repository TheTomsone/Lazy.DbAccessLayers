using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider
{
    public class DbPropertiesProvider : IDbPropertiesProvider
    {
        public string Tablename<TModel>()
        {
            return typeof(TModel).Name;
        }
        public PropertyInfo[] Properties<TModel>()
        {
            return typeof(TModel).GetProperties();
        }
        public PropertyInfo Property<TModel>(string propName)
        {
            return Properties<TModel>().First(property => property.Name == propName);
        }
    }
}

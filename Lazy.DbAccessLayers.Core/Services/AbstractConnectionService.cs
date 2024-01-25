using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services
{
    public class AbstractConnectionService(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IRegexFormatter? regexFormatter = null
        ) : AbstractService(propertiesProvider, regexFormatter)
    {
        protected readonly IDbConnectionProvider _connectionProvider = connectionProvider;
    }
}

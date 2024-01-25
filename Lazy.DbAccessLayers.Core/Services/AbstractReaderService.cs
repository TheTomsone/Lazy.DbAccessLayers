using Lazy.DbAccessLayers.Core.DataBaseContext.Connections;
using Lazy.DbAccessLayers.Core.DataBaseContext.PropertiesProvider;
using Lazy.DbAccessLayers.Core.Services.Mapping;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services
{
    public class AbstractReaderService(
        IDbPropertiesProvider propertiesProvider,
        IDbConnectionProvider connectionProvider,
        IMapper mapper,
        IRegexFormatter? regexFormatter = null
        ) : AbstractConnectionService(propertiesProvider, connectionProvider, regexFormatter)
    {
        protected readonly IMapper _mapper = mapper;
    }
}

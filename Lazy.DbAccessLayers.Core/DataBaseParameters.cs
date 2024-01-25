using Lazy.DbAccessLayers.Core.AbstractCentralizedFactory;
using Lazy.DbAccessLayers.Core.Services.Regexes;
using System.Data.Common;

namespace Lazy.DbAccessLayers.Core
{
    public class DataBaseParameters
    {
        public string ConnectionString { get; set; }
        public FactoryType FactoryType { get; set; }
        public RegexType RegexType { get; set; }
    }
}

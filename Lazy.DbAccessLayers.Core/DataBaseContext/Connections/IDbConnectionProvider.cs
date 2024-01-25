using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.Connections
{
    public interface IDbConnectionProvider
    {
        DbConnection Connection { get; }
        IDbConnectionProvider ChangeConnection(string connectionString);
    }
}

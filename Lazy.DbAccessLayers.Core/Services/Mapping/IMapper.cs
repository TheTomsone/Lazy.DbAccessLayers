using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Mapping
{
    public interface IMapper
    {
        TModel Map<TModel>(IDataReader reader) where TModel : class, new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Interfaces
{
    public interface IMultiReader
    {
        IEnumerable<TModel> Sync<TModel>() where TModel : class, new();
        Task<IEnumerable<TModel>> Async<TModel>() where TModel : class, new();
    }
}

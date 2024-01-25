using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Interfaces
{
    public interface ISingleReader
    {
        TModel Sync<TModel>(int id, string compositeKey = "") where TModel : class, new();
        Task<TModel> Async<TModel>(int id, string compositeKey = "") where TModel : class, new();
    }
}

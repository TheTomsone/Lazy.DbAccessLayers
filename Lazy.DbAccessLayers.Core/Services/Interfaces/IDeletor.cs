using Lazy.DbAccessLayers.Core.Services.Regexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Interfaces
{
    public interface IDeletor
    {
        int Sync<TModel>(int id, string compositeKey = "") where TModel : class, new();
        Task<int> Async<TModel>(int id, string compositeKey = "") where TModel : class, new();
    }
}

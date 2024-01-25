using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Interfaces
{
    public interface ICreator
    {
        int Sync<TModel>(TModel model, bool isComposite = false) where TModel : class, new();
        Task<int> Async<TModel>(TModel model, bool isComposite = false) where TModel : class, new();
    }
}

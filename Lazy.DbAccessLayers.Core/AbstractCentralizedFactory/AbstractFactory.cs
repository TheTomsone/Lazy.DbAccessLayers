using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.AbstractCentralizedFactory
{
    public abstract class AbstractFactory<TInstance> : IFactory<TInstance>
    {
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory<TInstance>, new()
        {
            return new TFactory();
        }
        public abstract TInstance CreateInstance();
    }
}

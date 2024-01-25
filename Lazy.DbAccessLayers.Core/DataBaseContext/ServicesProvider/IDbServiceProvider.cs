using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.ServicesProvider
{
    public interface IDbServiceProvider
    {
        IMapper Mapper { get; }
        ICreator Creator { get; }
        IMultiReader MultiReader { get; }
        ISingleReader SingleReader { get; }
        IUpdator Updator { get; }
        IDeletor Deletor { get; }

    }
}

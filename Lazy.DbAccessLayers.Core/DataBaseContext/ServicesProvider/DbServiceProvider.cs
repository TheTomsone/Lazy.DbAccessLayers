using Lazy.DbAccessLayers.Core.Services.Interfaces;
using Lazy.DbAccessLayers.Core.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.DataBaseContext.ServicesProvider
{
    public class DbServiceProvider(
        IMapper mapper,
        ICreator creator,
        IMultiReader multiReader,
        ISingleReader singleReader,
        IUpdator updator,
        IDeletor deletor
        ) : IDbServiceProvider
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICreator _creator = creator;
        private readonly IMultiReader _multiReader = multiReader;
        private readonly ISingleReader _singleReader = singleReader;
        private readonly IUpdator _updator = updator;
        private readonly IDeletor _deletor = deletor;

        public IMapper Mapper => _mapper;
        public ICreator Creator => _creator;
        public IMultiReader MultiReader => _multiReader;
        public ISingleReader SingleReader => _singleReader;
        public IUpdator Updator => _updator;
        public IDeletor Deletor => _deletor;
    }
}

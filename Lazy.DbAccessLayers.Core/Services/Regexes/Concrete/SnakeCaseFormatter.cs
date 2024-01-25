using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lazy.DbAccessLayers.Core.Services.Regexes;

namespace Lazy.DbAccessLayers.Core.Services.Regexes.Concrete
{
    public class SnakeCaseFormatter : AbstractRegexFormatter, IRegexFormatter
    {
        protected override string Pattern => @"([a-z])([A-Z])";
        protected override MatchEvaluator Evaluator => m => $"{m.Groups[1].Value}_{m.Groups[2].Value}";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lazy.DbAccessLayers.Core.Services.Regexes
{
    public abstract class AbstractRegexFormatter : IRegexFormatter
    {
        protected abstract string Pattern { get; }
        protected abstract MatchEvaluator Evaluator { get; }
        public string Format(string value)
        {
            return Regex.Replace(value, Pattern, Evaluator);
        }
    }
}

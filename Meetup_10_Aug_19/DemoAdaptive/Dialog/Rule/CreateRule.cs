using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs.Adaptive;

namespace DemoAdaptive.Dialog
{
    public partial class RootDialog
    {
        public List<IRule> CreateRule()
        {
            var rules = new List<IRule> {BookRule(), WeatherRules()};

            return rules;
        }
    }
}
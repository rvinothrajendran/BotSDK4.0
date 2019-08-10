using System.Collections.Generic;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Rules;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Steps;
using IRule = Microsoft.Bot.Builder.Dialogs.Adaptive.IRule;

namespace DemoAdaptive.Dialog
{
    public partial class RootDialog
    {
        #region BookFlight

        public IRule WeatherRules()
        {
            return new IntentRule
            {
                Intent = "Weather", Steps = WeatherSteps()
            };
        }

        private List<IDialog> WeatherSteps()
        {
            var steps = new List<IDialog>();

            var city = new TextInput
            {
                Property = "Weather.City",
                Prompt = new ActivityTemplate("[WeatherCity]")
            };

            steps.Add(city);
            steps.Add(new SendActivity("[WeatherRange]"));
            steps.Add(new EndDialog());
            return steps;
        }

        #endregion
    }
}
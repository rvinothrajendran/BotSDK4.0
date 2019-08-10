using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Recognizers;

namespace DemoAdaptive.Dialog
{
    public partial class RootDialog
    {
        private static IRecognizer CreateRegexRecognizer()
        {
            var recognizer = new RegexRecognizer
            {
                Intents = new Dictionary<string, string>
                {
                    {"Book", "(?i)book"},
                    {"Weather","(?i)weather"},
                }
            };
            return recognizer;
        }
    }
}
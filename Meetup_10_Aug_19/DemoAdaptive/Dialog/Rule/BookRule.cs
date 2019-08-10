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

        public IRule BookRule()
        {
            return new IntentRule
            {
                Intent = "Book", Steps = BookSteps()
            };
        }

        private List<IDialog> BookSteps()
        {
            var steps = new List<IDialog>();

            var userName = new TextInput
            {
                Property = "user.userProfile.Name",
                Prompt = new ActivityTemplate("[AskForName]")
            };

            var cityInput = new TextInput
            {
                Property = "user.userProfile.destination",
                Prompt = new ActivityTemplate("[DestinationPrompt]")
            };

            steps.Add(userName);
            steps.Add(new SendActivity("[AckName]"));
            steps.Add(cityInput);
            steps.Add(new SendActivity("[ConfirmInfo]"));

            var confirm = new ConfirmInput
            {
                Prompt = new ActivityTemplate("[ConfirmPrompt]"),
                Property = "turn.finalConfirmation"
            };
            steps.Add(confirm);

            var condition = new IfCondition
            {
                Condition = "turn.finalConfirmation == true",
                Steps = new List<IDialog>() {new SendActivity("[ConfirmInfo]") },
                ElseSteps = new List<IDialog> {new SendActivity("[NotConfirm]") }
            };
            steps.Add(condition);
            steps.Add(new EndDialog());
            return steps;
        }

        #endregion
    }
}
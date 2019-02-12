using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeetpupDialog.ChildDialog;
using MeetpupDialog.MainDialog;
using MeetpupDialog.Execute;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace MeetpupDialog
{
    public partial class MeetpupDialogBot
    {
        private DialogSet _dialogSet;

        private void ExecuteMainDialog(IStatePropertyAccessor<DialogState> dlgstate)
        {

            var waterFallSteps = new WaterfallStep[]
            {
                RunMainDialog,
                EndMainDlg
            };

            _dialogSet = new DialogSet(dlgstate);
            _dialogSet.Add(new TextPrompt("text"));
            _dialogSet.Add(new NumberPrompt<int>("number"));
            _dialogSet.Add(new ChoicePrompt("choice"));
            _dialogSet.Add(new ExecuteChildDialog(nameof(UserForm), UserForm.PromptProperties()));
            _dialogSet.Add(new ExecuteChildDialog(nameof(PizzaOrder), PizzaOrder.PromptProperties()));
            _dialogSet.Add(new ExecuteChildDialog(nameof(RootDialog),RootDialog.PromptProperties()));
            _dialogSet.Add(new WaterfallDialog("main", waterFallSteps));

        }

        private async Task<DialogTurnResult> RunMainDialog(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            return await stepcontext.BeginDialogAsync(nameof(RootDialog), null, cancellationtoken);
        }

        private async Task<DialogTurnResult> EndMainDlg(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            if(stepcontext.Result is IDictionary<string,object> result)
            {
                var userform = (IDictionary<string, object>)result[nameof(UserForm)];
                var display = DisplayResult(userform, stepcontext, cancellationtoken);

                var pizzaform = (IDictionary<string, object>)result[nameof(PizzaOrder)];
                display += DisplayResult(pizzaform, stepcontext, cancellationtoken);

                await stepcontext.Context.SendActivityAsync(display, cancellationToken: cancellationtoken);
            }
            return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);
        }

        private static string DisplayResult(IDictionary<string, object> result, WaterfallStepContext stepcontext,
            CancellationToken cancellationtoken)
        {
            string res = string.Empty;
            foreach (var prop in result)
            {
                res += "\n" + prop.Key + " : " + prop.Value;
            }
            return res;
        }
    }
}

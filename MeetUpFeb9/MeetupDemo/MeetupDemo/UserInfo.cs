using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace MeetupDemo
{
    public partial class MeetupDemoBot
    {
        private DialogSet _dialogSet;
        private void PrepareDialogSet(IStatePropertyAccessor<DialogState> dlgState)
        {
            var waterfallStep = new WaterfallStep[]
            {
                ReadUserNameAsync,
                ReadMobileNumberAsync
            };

            _dialogSet = new DialogSet(dlgState);
            _dialogSet.Add(new TextPrompt("text"));
            _dialogSet.Add(new NumberPrompt<int>("number"));
            _dialogSet.Add(new WaterfallDialog("mainDlg", waterfallStep));

        }
        private async Task<DialogTurnResult> ReadUserNameAsync(WaterfallStepContext waterfallStepContext,
            CancellationToken cancellation)
        {
            return await waterfallStepContext.PromptAsync("text", new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please enter the Name")
            }, cancellation);
        }

        private async Task<DialogTurnResult> ReadMobileNumberAsync(WaterfallStepContext waterfallStepContext,
            CancellationToken cancellation)
        {
            var userName = (string)waterfallStepContext.Result;

            return await waterfallStepContext.PromptAsync("number", new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please enter the Mobile Number")
            }, cancellation);
        }
    }
}

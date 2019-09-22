using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveCardPromptSample.DialogTemplate
{
    public class DialogTemplate<T> : ActivityHandler where T : Dialog
    {
        protected readonly BotState _conversationState;
        protected readonly BotState _userState;
        protected readonly Dialog _dialog;
        public DialogTemplate(ConversationState conversationState,UserState userState,T dialog)
        {
            _conversationState = conversationState;
            _userState = userState;
            _dialog = dialog;           
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            await _dialog.Run(turnContext, _conversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }
    }

    public static class DialogExtensions
    {
        public static async Task Run(this Dialog dialog,ITurnContext turnContext, IStatePropertyAccessor<DialogState> dlgState,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dialogSet = new DialogSet(dlgState);
            dialogSet.Add(dialog);

            var dlgContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);
            var results = await dlgContext.ContinueDialogAsync((cancellationToken));
            if (results.Status == DialogTurnStatus.Empty)
            {
                await dlgContext.BeginDialogAsync(dialog.Id, null, cancellationToken);
            }
        }
    }
}

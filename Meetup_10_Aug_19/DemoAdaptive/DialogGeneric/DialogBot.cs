using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace DemoAdaptive.DialogGeneric
{
    public class DialogBot<T> : ActivityHandler where T : Microsoft.Bot.Builder.Dialogs.Dialog
    {
        protected readonly Microsoft.Bot.Builder.Dialogs.Dialog Dialog;
        protected readonly BotState ConversationState;
        protected readonly BotState UserState;
        private readonly DialogManager _dialogManager;

        public DialogBot(ConversationState conversationState, UserState userState, T dialog)
        {
            Dialog = dialog;
            ConversationState = conversationState;
            UserState = userState;
            _dialogManager = new DialogManager(Dialog);
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            await _dialogManager.OnTurnAsync(turnContext,cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            //await base.OnTurnAsync(turnContext, cancellationToken);

            //await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            //await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        //protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    await Dialog.Run(turnContext,
        //        ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        //    //await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
        //}

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello"), cancellationToken);
                }
            }
        }
    }

    public static class DialogExtensions
    {
        public static async Task Run(this Microsoft.Bot.Builder.Dialogs.Dialog dialog,
            ITurnContext turnContext, IStatePropertyAccessor<DialogState> dlgState,
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
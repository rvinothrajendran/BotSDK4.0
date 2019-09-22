using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using AdaptiveCardPromptSample.DialogTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveCardPromptSample.Welcome
{
    public partial class WelcomeRoomDialog<T> : DialogTemplate<T> where T : Dialog
    {
        public WelcomeRoomDialog(ConversationState conversationState, UserState userState, T dialog) :
            base(conversationState, userState, dialog)
        {

        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync("Welcome to AdaptiveCardPrompt Soultion,send Hi message to continue ...",
                        cancellationToken:cancellationToken);
                }
            }
        }
    }
}

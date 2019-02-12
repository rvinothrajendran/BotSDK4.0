// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using MeetpupDialog.State;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace MeetpupDialog
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service. Transient lifetime services are created
    /// each time they're requested. Objects that are expensive to construct, or have a lifetime
    /// beyond a single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public partial class MeetpupDialogBot : IBot
    {
        private StateAccessors _stateAccessors;
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>                        
        public MeetpupDialogBot(StateAccessors stateAccessors)
        {
            _stateAccessors = stateAccessors;
            ExecuteMainDialog(_stateAccessors.DlgState);
        }

        /// <summary>
        /// Every conversation turn calls this method.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var dlgcontext = await _dialogSet.CreateContextAsync(turnContext, cancellationToken);
                if (dlgcontext.ActiveDialog == null)
                {
                    await dlgcontext.BeginDialogAsync("main", cancellationToken: cancellationToken);
                }
                else
                {
                    await dlgcontext.ContinueDialogAsync(cancellationToken: cancellationToken);
                }

                await _stateAccessors.ConversationState.SaveChangesAsync(turnContext, cancellationToken: cancellationToken);
                // Echo back to the user whatever they typed.             
                //await turnContext.SendActivityAsync("Hello World", cancellationToken: cancellationToken);
            }
        }
    }
}

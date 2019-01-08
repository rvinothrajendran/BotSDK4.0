// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace BotPrompt
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
    public class BotPromptBot : IBot
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        ///
        private readonly DialogSet _dialogSet;
        private readonly StateAccessor _stateAccessor;

        private readonly string DlgMainId = "MainDialog";
        private readonly string DlgNameId = "NameDlg";
        private readonly string DlgMobileId = "MobileDlg";
        private readonly string DlgLanguageId = "LanguageListDlg";
        private readonly string DlgDateTimeId = "DateTimeDlg";

        public BotPromptBot(StateAccessor stateAccessor)
        {
            _stateAccessor = stateAccessor;
            _dialogSet = new DialogSet(stateAccessor.DlgState);
            _dialogSet.Add(new TextPrompt(DlgNameId));
            _dialogSet.Add(new NumberPrompt<int>(DlgMobileId));
            _dialogSet.Add(new ChoicePrompt(DlgLanguageId));
            _dialogSet.Add(new DateTimePrompt(DlgDateTimeId));

            var waterfallSteps = new WaterfallStep[]
            {
                UserNameAsync,
                GetUserNameAsync,
                MobileNumberAsync,
                SelectLanguageList,
                DateTimeAsync,
            };

            _dialogSet.Add(new WaterfallDialog(DlgMainId, waterfallSteps));
            
            

        }

        private async Task<DialogTurnResult> GetUserNameAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            var name = (string)stepcontext.Result;

            return await stepcontext.PromptAsync(DlgMobileId, new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please enter the mobile No"),
                RetryPrompt = MessageFactory.Text("Enter Valid mobile No")
            }, cancellationtoken);
        }

        private async Task<DialogTurnResult> UserNameAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            return await stepcontext.PromptAsync(DlgNameId, new PromptOptions
            {
                Prompt = MessageFactory.Text("Hello !!!, Please enter the Name")
                
            }, cancellationtoken);
        }

        private async Task<DialogTurnResult> MobileNumberAsync(WaterfallStepContext stepContext,
            CancellationToken cancellationtoken)
        {
            var mobileNo = stepContext.Result;

            var newMovieList = new List<string> {" Tamil ", " English ", " kaanda "};
            
            return await stepContext.PromptAsync(DlgLanguageId, new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please select the Language"),
                Choices = ChoiceFactory.ToChoices(newMovieList),
                RetryPrompt = MessageFactory.Text("Select from the List")
            },cancellationtoken);
        }

        private async Task<DialogTurnResult> SelectLanguageList(WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            var choice = (FoundChoice) stepContext.Result;

            return await stepContext.PromptAsync(DlgDateTimeId, new PromptOptions()
            {
                Prompt = MessageFactory.Text("Please select the Date")
            },cancellationToken);
        }

        private async Task<DialogTurnResult> DateTimeAsync(WaterfallStepContext stepcontext, CancellationToken cancellationtoken)
        {
            var datetime = stepcontext.Result;

            return await stepcontext.EndDialogAsync(cancellationToken: cancellationtoken);
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

                DialogContext dlgContext =
                    await _dialogSet.CreateContextAsync(turnContext, cancellationToken: cancellationToken);

                if (dlgContext != null && dlgContext.ActiveDialog is null)
                {
                    await dlgContext.BeginDialogAsync(DlgMainId, null, cancellationToken);
                }
                else if (dlgContext != null && dlgContext.ActiveDialog != null)
                {
                    await dlgContext.ContinueDialogAsync(cancellationToken);
                }

                await _stateAccessor.Conversation.SaveChangesAsync(turnContext, false, cancellationToken);

                // Echo back to the user whatever they typed.             
               // await turnContext.SendActivityAsync("Hello World", cancellationToken: cancellationToken);
            }
        }
    }
}

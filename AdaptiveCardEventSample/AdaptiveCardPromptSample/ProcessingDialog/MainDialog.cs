using Bot.AdaptiveCard.Prompt;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using AdaptiveCardPromptSample.Welcome;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveCardPromptSample
{
    public class MainDialog : ComponentDialog
    {
        static string AdaptivePromptId = "adaptive";
        public MainDialog() : base(nameof(MainDialog))
        {
            AddDialog(new AdaptiveCardPrompt(AdaptivePromptId));
            AddDialog(new TextPrompt(nameof(TextPrompt)));          
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                UserFormAsync,
                ResultUserFormAsync,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> UserFormAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var cardJson = PrepareCard.ReadCard("userform.json");

            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                    Text = "Please fill this form",
                }
            };

            return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);           
        }

        private static async Task<DialogTurnResult> ResultUserFormAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var value = stepContext.Result.ToString();
            
            var promptOptions = new PromptOptions
            {
                Prompt = MessageFactory.Text(value)
            };
            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }      
    }
}

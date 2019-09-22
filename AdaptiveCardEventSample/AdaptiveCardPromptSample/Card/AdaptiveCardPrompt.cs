using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.AdaptiveCard.Prompt
{
    public class AdaptiveCardPrompt : Prompt<JObject>
    {
        public AdaptiveCardPrompt(string dialogId,PromptValidator<JObject> validator=null)
            : base(dialogId,validator)
        {

        }
        protected override async Task OnPromptAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, bool isRetry, CancellationToken cancellationToken = default)
        {
            if(turnContext == null)
            {
                throw new ArgumentException(nameof(turnContext));
            }

            if(options == null)
            {
                throw new ArgumentException(nameof(options));
            }

            if(isRetry && options.Prompt !=null)
            {
                await turnContext.SendActivityAsync(options.RetryPrompt, cancellationToken).ConfigureAwait(false);
            }
            else if(options.Prompt != null)
            {
                await turnContext.SendActivityAsync(options.Prompt, cancellationToken).ConfigureAwait(false);
            }
        }

        protected override Task<PromptRecognizerResult<JObject>> OnRecognizeAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, CancellationToken cancellationToken = default)
        {
            if(turnContext == null)
            {
                throw new ArgumentException(nameof(turnContext));
            }

            if(turnContext.Activity == null)
            {
                throw new ArgumentException(nameof(turnContext));
            }

            var result = new PromptRecognizerResult<JObject>();

            if(turnContext.Activity.Type == ActivityTypes.Message)
            {
                if (turnContext.Activity.Value != null)
                {
                    if(turnContext.Activity.Value is JObject)
                    {
                        result.Value = turnContext.Activity.Value as JObject;
                        result.Succeeded = true;
                    }                    
                }
               
            }

            return Task.FromResult(result);
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace NewBotSample
{
    public class HelloBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                await turnContext.SendActivityAsync("Hello Welcome to Bot new 4.0 Sample", cancellationToken: cancellationToken);
            }
        }
    }
}

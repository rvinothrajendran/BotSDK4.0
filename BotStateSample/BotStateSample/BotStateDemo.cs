using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace BotStateSample
{
    public class BotStateDemo : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            await turnContext.SendActivityAsync("Hello Bot SDK 4.0", cancellationToken: cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace BotWelcomeMsg
{
    public class WelcomeBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            await turnContext.SendActivityAsync("Hello Welcome to Bot",cancellationToken:cancellationToken);
        }
    }
}

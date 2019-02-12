using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace MeetupDemo
{
    public class UserLogMiddleware : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await turnContext.SendActivityAsync("Before Hello Welcome to Middelware",cancellationToken: cancellationToken);
            await next(cancellationToken);
            await turnContext.SendActivityAsync("After Hello Welcome to Middelware", cancellationToken: cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace Middleware
{
    public class MiddlewareLogger : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await turnContext.SendActivityAsync("Before MiddlewareLogger", cancellationToken: cancellationToken);
            await next(cancellationToken);
            await turnContext.SendActivityAsync("After MiddlewareLogger", cancellationToken: cancellationToken);
        }
    }
}

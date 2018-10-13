using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace Middleware
{
    public class MiddlewareErrorHandler : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await turnContext.SendActivityAsync("Before MiddlewareErrorHandler", cancellationToken: cancellationToken);
            await next(cancellationToken);
            await turnContext.SendActivityAsync("After MiddlewareErrorHandler", cancellationToken: cancellationToken);
        }
    }
}

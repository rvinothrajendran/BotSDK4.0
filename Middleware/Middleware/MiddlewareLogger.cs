using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace Middleware
{
    public class MiddlewareLogger : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await turnContext.SendActivityAsync("Logger Handler Middleware (Before) ",
                cancellationToken: cancellationToken);

            await next(cancellationToken);

            await turnContext.SendActivityAsync("Logger Handler Middleware (After)",
                cancellationToken: cancellationToken);
        }
    }
}

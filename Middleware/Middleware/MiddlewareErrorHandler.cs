using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace Middleware
{
    public class MiddlewareErrorHandler : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await turnContext.SendActivityAsync("Error Handler Middleware (Before) ",
                cancellationToken: cancellationToken);

            await next(cancellationToken);

            await turnContext.SendActivityAsync("Error Handler Middleware (After)",
                cancellationToken: cancellationToken);
        }
    }
}

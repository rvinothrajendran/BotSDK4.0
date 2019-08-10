using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Debugging;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;

namespace DemoAdaptive
{
    public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
    {
        public AdapterWithErrorHandler(IStorage storage,UserState userState,ConversationState conversationState)
        {
            this.UseStorage(storage);
            this.UseState(userState, conversationState);
            //this.Use(new RegisterClassMiddleware<IMessageActivityGenerator>(new TextMessageActivityGenerator()));
            //this.UseDebugger( 3978, events: new Events<AdaptiveEvents>());
        }
    }
}
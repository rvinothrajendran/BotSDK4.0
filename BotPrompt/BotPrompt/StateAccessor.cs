using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace BotPrompt
{
    public class StateAccessor
    {
        public ConversationState Conversation;
        public IStatePropertyAccessor<DialogState> DlgState;

        public StateAccessor()
        {
            Conversation = new ConversationState(new MemoryStorage());
            DlgState = Conversation.CreateProperty<DialogState>("DlgState");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace MeetupDemo
{
    public class StateAccessor
    {
        public IStatePropertyAccessor<DialogState> DlgState;
        public ConversationState ConversationState;
        public StateAccessor()
        {
            ConversationState = new ConversationState(new MemoryStorage());
            DlgState = ConversationState.CreateProperty<DialogState>("DlgState");
        }
    }
}

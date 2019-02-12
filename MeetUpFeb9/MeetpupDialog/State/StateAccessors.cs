using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace MeetpupDialog.State
{
    public class StateAccessors
    {
        public IStatePropertyAccessor<DialogState> DlgState;

        public ConversationState ConversationState;

        public StateAccessors()
        {
            ConversationState = new ConversationState(new MemoryStorage());
            DlgState = ConversationState.CreateProperty<DialogState>("DlgState");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Newtonsoft.Json.Serialization;

namespace BotStateSample
{

    public enum Mode
    {
        Name =0,
        Email,
        Completed 
    }
    public class StateInfo
    {
        public Mode CurrentMode { get; set; } = Mode.Name;
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class StateHelper
    {
        public IStatePropertyAccessor<StateInfo> CurrentState;

        public IStatePropertyAccessor<UserInfo> CurrentUserInfo;

        public ConversationState ConversationState;

        public UserState UserState;

        public string CurrentStateKey = nameof(StateHelper) + ".StateInfo";
        public string CurrentUserKey = nameof(UserState) + ".UserInfo";

        public StateHelper()
        {
            ConversationState = new ConversationState(new MemoryStorage());
            CurrentState = ConversationState.CreateProperty<StateInfo>(CurrentStateKey);

            UserState = new UserState(new MemoryStorage());
            CurrentUserInfo = UserState.CreateProperty<UserInfo>(CurrentUserKey);
        }
    }
}

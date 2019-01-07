using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Newtonsoft.Json.Serialization;

namespace BotStateSample
{

    public enum QuestionOrder
    {
        Name,
        Email,
        Mobile,
        Completed
    }

    public class TrackingState
    {
        public QuestionOrder Order = QuestionOrder.Name;
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Mobile { get; set; }
    }

    public class StateAccessor
    {

        public ConversationState converstate;
        public IStatePropertyAccessor<TrackingState> currentTracking;

        public IStatePropertyAccessor<UserInfo> currentUser;

        public UserState userState;

        public StateAccessor()
        {
            converstate = new ConversationState(new MemoryStorage());
            currentTracking = converstate.CreateProperty<TrackingState>("TrackingState");

            userState = new UserState(new MemoryStorage());
            currentUser = userState.CreateProperty<UserInfo>("CurrentUser");
        }
        
    }

}

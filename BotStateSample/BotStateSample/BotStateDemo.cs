using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace BotStateSample
{
    public class BotStateDemo : IBot
    {
        private readonly StateHelper _stateHelper;

        public BotStateDemo(StateHelper stateHelper)
        {
            _stateHelper = stateHelper;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            var currentState = await _stateHelper.CurrentState.GetAsync(turnContext, () => new StateInfo(), cancellationToken);

            var userdata = await _stateHelper.CurrentUserInfo.GetAsync(turnContext, () => new UserInfo(), cancellationToken);

            switch (currentState.CurrentMode)
            {
                case Mode.Name:
                    await turnContext.SendActivityAsync("Hello What is your name", cancellationToken: cancellationToken);
                    currentState.CurrentMode = Mode.Email;
                    await _stateHelper.CurrentState.SetAsync(turnContext, currentState, cancellationToken);
                    await _stateHelper.ConversationState.SaveChangesAsync(turnContext,cancellationToken: cancellationToken);
                    break;
                case Mode.Email:

                    userdata.Name = turnContext.Activity.Text;
                    await _stateHelper.CurrentUserInfo.SetAsync(turnContext, userdata, cancellationToken);
                    await _stateHelper.UserState.SaveChangesAsync(turnContext, cancellationToken: cancellationToken);

                    var strInfo = "Hello " + userdata.Name + " , Please enter the email";
                    await turnContext.SendActivityAsync(strInfo, cancellationToken: cancellationToken);
                    currentState.CurrentMode = Mode.Completed;
                    await _stateHelper.CurrentState.SetAsync(turnContext, currentState, cancellationToken);
                    await _stateHelper.ConversationState.SaveChangesAsync(turnContext, cancellationToken: cancellationToken);
                    break;
                case Mode.Completed:

                    userdata.Email = turnContext.Activity.Text;
                    await _stateHelper.CurrentUserInfo.SetAsync(turnContext, userdata, cancellationToken);
                    await _stateHelper.UserState.SaveChangesAsync(turnContext, cancellationToken: cancellationToken);

                    var User = "Your Name :" + userdata.Name + " , Email Id : " + userdata.Email;
                    await turnContext.SendActivityAsync(User, cancellationToken: cancellationToken);
                    await turnContext.SendActivityAsync("Process has completed", cancellationToken: cancellationToken);
                    break;
            }
        }
    }
}

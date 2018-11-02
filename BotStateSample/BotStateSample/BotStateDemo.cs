using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace BotStateSample
{
    public class BotStateDemo : IBot
    {
        private StateAccessor _StateAccssor;
        public BotStateDemo(StateAccessor stateAccessor)
        {
            _StateAccssor = stateAccessor;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            var currentState = await _StateAccssor.currentTracking.GetAsync(turnContext, () => new TrackingState());

            var currentUser = await _StateAccssor.currentUser.GetAsync(turnContext, () => new UserInfo());

            switch (currentState.Order)
            {
                case QuestionOrder.Name:
                    await turnContext.SendActivityAsync("Please enter the Name");

                    currentState.Order = QuestionOrder.Email;
                    await _StateAccssor.currentTracking.SetAsync(turnContext, currentState);
                    await _StateAccssor.converstate.SaveChangesAsync(turnContext);

                    break;
                case QuestionOrder.Email:

                    currentUser.Name = turnContext.Activity.Text;
                    await _StateAccssor.currentUser.SetAsync(turnContext, currentUser);
                    await _StateAccssor.userState.SaveChangesAsync(turnContext);

                    await turnContext.SendActivityAsync("Please enter the EMail");
                    currentState.Order = QuestionOrder.Mobile;
                    await _StateAccssor.currentTracking.SetAsync(turnContext, currentState);
                    await _StateAccssor.converstate.SaveChangesAsync(turnContext);


                    break;
                case QuestionOrder.Mobile:

                    currentUser.Email = turnContext.Activity.Text;
                    await _StateAccssor.currentUser.SetAsync(turnContext, currentUser);
                    await _StateAccssor.userState.SaveChangesAsync(turnContext);

                    await turnContext.SendActivityAsync("Please enter the Mobile");
                    currentState.Order = QuestionOrder.Completed;
                    await _StateAccssor.currentTracking.SetAsync(turnContext, currentState);
                    await _StateAccssor.converstate.SaveChangesAsync(turnContext);
                    break;
                case QuestionOrder.Completed:

                    currentUser.Mobile = turnContext.Activity.Text;
                    await _StateAccssor.currentUser.SetAsync(turnContext, currentUser);
                    await _StateAccssor.userState.SaveChangesAsync(turnContext);

                    var RequestUserInfo = await _StateAccssor.currentUser.GetAsync(turnContext, () => new UserInfo());

                    var userData = $"User Name : {RequestUserInfo.Name} , Email : {RequestUserInfo.Email} , Mobile : {RequestUserInfo.Mobile}"; ;

                    await turnContext.SendActivityAsync("Hey your task has done , Please check");
                    await turnContext.SendActivityAsync(userData);

                    break;
            }
        }
    }
}

using System.Collections.Generic;
using MeetpupDialog.Model;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace MeetpupDialog.ChildDialog
{
    public static class PizzaOrder
    {
        public static List<Property> PromptProperties()
        {
            var pizzalist = new PromptOptions
            {
                Choices = new List<Choice> {new Choice("Plain Pizza"), new Choice("Pizza with Mushrooms"), new Choice("Pizza3")},
                Prompt = MessageFactory.Text("Please select the Pizza Type")
            };

            var lstpropery = new List<Property>
            {
                new Property("choice","PizzaName",pizzalist)
            };

            return lstpropery;
        }
    }
}
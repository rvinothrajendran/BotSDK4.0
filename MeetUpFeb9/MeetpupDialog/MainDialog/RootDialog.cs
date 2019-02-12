using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetpupDialog.ChildDialog;
using MeetpupDialog.Model;


namespace MeetpupDialog.MainDialog
{
    public static class RootDialog
    {
        public static List<Property> PromptProperties()
        {
            var lstProperty = new List<Property>()
            {
                new Property(nameof(UserForm),nameof(UserForm)),
                new Property(nameof(PizzaOrder),nameof(PizzaOrder))
            };

            return lstProperty;
        }
    }
}

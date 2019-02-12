using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetpupDialog.Model;

namespace MeetpupDialog.ChildDialog
{
    public static class UserForm
    {
        public static List<Property> PromptProperties()
        {
            var lstproperty = new List<Property>
            {
                new Property("text", "FirstName", "Please enter the user Name"),
                new Property("number", "moblieNo", "Please enter the mobile Number")
            };

            return lstproperty;
        }
    }
}

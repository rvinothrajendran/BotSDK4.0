using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace MeetpupDialog.Model
{
    public class Property
    {
        public string Id;

        public string Name;

        public PromptOptions Pmoptions;

        public Property(string id, string name, PromptOptions pmoptions)
        {
            this.Id = id;
            Name = name;
            this.Pmoptions = pmoptions;
        }

        public Property(string id,string name,string pmtext=null) : 
            this(id,name, new PromptOptions { Prompt = MessageFactory.Text(pmtext) }) 
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeetpupDialog.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Remotion.Linq.Clauses.ResultOperators;

namespace MeetpupDialog.Execute
{
    public class ExecuteChildDialog : Dialog
    {
        private readonly List<Property> _executeProperty;
        private string Property = "CurrentProperty";
        private static string MyDir = "StoreMyValue";

        public ExecuteChildDialog(string dialogId,List<Property> lstProperties) : base(dialogId)
        {
            _executeProperty = lstProperties;
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ExecuteProperty(dc, cancellation: cancellationToken);
        }

        public override async Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = new CancellationToken())
        {
            return await ExecuteProperty(dc, cancellation: cancellationToken);
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var exeProp = (string) dc.ActiveDialog.State[Property];
            var value = GetStoredValue(dc.ActiveDialog);

            if (result is FoundChoice resultChoice)
            {
                value[exeProp] = resultChoice.Value;
            }
            else
            {
                value[exeProp] = result;
            }
            
            return await ExecuteProperty(dc, cancellation: cancellationToken);
        }

        private static IDictionary<string, object> GetStoredValue(DialogInstance dialogInstance)
        {
            if (!dialogInstance.State.TryGetValue(MyDir, out object value))
            {
                value = new Dictionary<string,object>();
                dialogInstance.State.Add(MyDir,value);
            }

            return (IDictionary<string, object>) value;
        }

        private async Task<DialogTurnResult> ExecuteProperty(DialogContext dlgContext, CancellationToken cancellation)
        {
            var executeprop = GetStoredValue(dlgContext.ActiveDialog);
            var unexecutedprop = _executeProperty.FirstOrDefault((item) => !executeprop.ContainsKey(item.Name));

            if (unexecutedprop != null)
            {
                dlgContext.ActiveDialog.State[Property] = unexecutedprop.Name;
                return await dlgContext.BeginDialogAsync(unexecutedprop.Id,unexecutedprop.Pmoptions,cancellationToken:cancellation);
            }
            else
            {
                return await dlgContext.EndDialogAsync(executeprop,cancellation);
            }
        }
    }
}

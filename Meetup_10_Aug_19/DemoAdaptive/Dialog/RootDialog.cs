using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Rules;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Steps;
using Microsoft.Bot.Builder.LanguageGeneration;

namespace DemoAdaptive.Dialog
{
    public partial class RootDialog : ComponentDialog
    {
        public RootDialog() : base(nameof(RootDialog))
        {
            string[] paths = {".", "Dialog", "RootDialog.lg"};
            var fullPath = Path.Combine(paths);

            var templateEngine = new TemplateEngine().AddFile(fullPath);

            var adaptiveRoot = new AdaptiveDialog(nameof(AdaptiveDialog))
            {
                Recognizer = CreateRegexRecognizer(),
                Generator = new TemplateEngineLanguageGenerator(templateEngine),
            };
            
            #region WelcomeMessage
                adaptiveRoot.Rules.Add(new ConversationUpdateActivityRule()
                {
                    Steps = new List<IDialog>()
                    {
                        new SendActivity("[Welcome]")
                    }
                });
            #endregion

            adaptiveRoot.Rules.Add(BookRule());
            adaptiveRoot.Rules.Add(WeatherRules());

            AddDialog(adaptiveRoot);

            InitialDialogId = nameof(AdaptiveDialog);
        }

        public sealed override Microsoft.Bot.Builder.Dialogs.Dialog AddDialog(IDialog dialog)
        {
            return base.AddDialog(dialog);
        }
    }
}
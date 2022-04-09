using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for displaying help.
    /// </summary>
    public class HelpController : BaseController
    {
        public HelpController(ViewManager viewManager, PopupManager popupManager) : base(viewManager, popupManager)
        {
        }

        public void ShowHelp()
        {
            IViewInitializationParameters initParams = new HelpViewInitializationParameters(new List<ListElement>()
            {
                new ListElement("O aplikacji", null, "", ShowAppExplanation),
                new ListElement("Objaśnienie ikon", null, "", ShowExerciseIconsExplanation)
            }, ReturnToPreviousView);

            ViewManager.OpenView(ViewType.Help, initParams);
        }

        private void ShowAppExplanation()
        {
            ViewManager.OpenView(ViewType.AppInfo,
                new ApplicationInfoViewInitializationParameters(ReturnToPreviousView, null));
        }

        private void ShowExerciseIconsExplanation()
        {
            ViewManager.OpenView(ViewType.IconInfo, new ApplicationInfoViewInitializationParameters(ReturnToPreviousView, null));
        }
    }
}
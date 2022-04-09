using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine.Events;


namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for the Profile View actions.
    /// </summary>
    public class ProfileController : BaseController
    {
        private readonly UnityAction _testButtonClicked;

        public ProfileController(ViewManager viewManager, PopupManager popupManager, UnityAction testButtonClicked) : base(
            viewManager, popupManager)
        {
            _testButtonClicked = testButtonClicked;
        }

        public void ShowProfile()
        {
            IViewInitializationParameters initParams =
                new ProfileViewInitializationParameters(new List<ListElement>()
                {
                    new ListElement("Kliknij tutaj, aby rozwiązać test sprawnościowy.", null, "Test", _testButtonClicked),
                },ReturnToPreviousView);
            ViewManager.OpenView(ViewType.Profile, initParams);
        }
    }
    
    
}

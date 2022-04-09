using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for displaying showing appropriate main menu view.
    /// </summary>
    public class MenuController : BaseController
    {
        public event Action ProfileButtonPressed;
        public event Action HelpButtonPressed;
        public event Action SettingsButtonPressed;
        public event Action TopMenuButtonPressed;
        public event Action BottomMenuButtonPressed;

        public MenuController(ViewManager viewManager, PopupManager popupManager) : base(viewManager, popupManager)
        {
        }

        public void ShowMenu(bool isPathInProgress)
        {
            string upperButtonText = isPathInProgress ? "Rozpocznij nową ścieżkę" : "Wyjdź na ścieżkę";
            string lowerButtonText = isPathInProgress ? "Kontynuuj ścieżkę" : "Zobacz ścieżkę";

            MainViewInitializationParameters initParams = new MainViewInitializationParameters(OnProfileButtonPressed,
                OnHelpButtonPressed, OnSettingsButtonPressed,
                () => OnTopMenuButtonPressed(isPathInProgress), () => OnBottomMenuButtonPressed(isPathInProgress),
                OnQuitApplicationButtonPressed, upperButtonText, lowerButtonText);

            ViewManager.OpenView(ViewType.Main, initParams);
        }

        private void OnProfileButtonPressed()
        {
            ProfileButtonPressed?.Invoke();
        }

        private void OnHelpButtonPressed()
        {
            HelpButtonPressed?.Invoke();
        }

        private void OnSettingsButtonPressed()
        {
            SettingsButtonPressed?.Invoke();
        }

        private void OnTopMenuButtonPressed(bool isPathInProgress)
        {
            TopMenuButtonPressed?.Invoke();
        }

        private void OnBottomMenuButtonPressed(bool isPathInProgress)
        {
            BottomMenuButtonPressed?.Invoke();
        }

        private void OnQuitApplicationButtonPressed()
        {
            Application.Quit();
        }
    }
}
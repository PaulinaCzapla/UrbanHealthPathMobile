using System;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for managing settings.
    /// </summary>
    public class SettingsController : BaseController
    {
        private readonly Settings _settings;
        
        public SettingsController(ViewManager viewManager, PopupManager popupManager, Settings settings) : base(viewManager, popupManager)
        {
            _settings = settings;
        }

        public void ShowSettings(Action returnButtonPressed, Action revertDefaultsButtonPressed)
        {
            IViewInitializationParameters initParams = new SettingsInitializationParameters(() => returnButtonPressed.Invoke(),
                () => revertDefaultsButtonPressed.Invoke(), OnChangeFontSizeButtonPressed, OnChangeThemeButtonPressed,
                OnToggleAudioButtonPressed);
            ViewManager.OpenView(ViewType.Settings, initParams);
        }

        private void OnChangeFontSizeButtonPressed()
        {
            
        }

        private void OnChangeThemeButtonPressed()
        {
            
        }

        private void OnToggleAudioButtonPressed()
        {
            _settings.IsAudioEnabled = !_settings.IsAudioEnabled;
        }
    }
}
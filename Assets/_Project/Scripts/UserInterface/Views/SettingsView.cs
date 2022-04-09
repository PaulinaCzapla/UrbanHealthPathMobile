using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a settings view. This object can be initialized with SettingsInitializationParameters.
    /// </summary>
    public class SettingsView : MonoBehaviour, IDisplayable, IInitializableView
    {
        [FormerlySerializedAs("revertButton")] [SerializeField] private Button _revertButton;
        [FormerlySerializedAs("returnButton")] [SerializeField] private Button _returnButton;
        [FormerlySerializedAs("fontButton")] [SerializeField] private Button _fontButton;
        [FormerlySerializedAs("themeButton")] [SerializeField] private Button _themeButton; 
        [FormerlySerializedAs("audioButton")] [SerializeField] private Button _audioButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;

        private void Awake()
        {
            headerPanel.Initialize("Ustawienia");
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is SettingsInitializationParameters init)
            {
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _revertButton.onClick.AddListener(() => init.RevertEvent?.Invoke());
                _fontButton.onClick.AddListener(() => init.FontEvent?.Invoke());
                _themeButton.onClick.AddListener(() => init.ThemeEvent?.Invoke());
                _audioButton.onClick.AddListener(() => init.AudioEvent?.Invoke());

                headerPanel.Initialize(init.HeaderText);
            }
        }

        public void Display()
        {
        }

        public void Hide()
        {
        }

        public void OnDisable()
        {
            _returnButton.onClick.RemoveAllListeners();
            _revertButton.onClick.RemoveAllListeners();
            _fontButton.onClick.RemoveAllListeners();
            _themeButton.onClick.RemoveAllListeners();
            _audioButton.onClick.RemoveAllListeners();
        }
    }
}
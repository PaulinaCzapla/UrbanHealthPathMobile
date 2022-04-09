using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a main view. This object can be initialized with MainViewInitializationParameters.
    /// It's extended by IPopupable interface, so it determines the size and position of a popup.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IInitializableView, IPopupable
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("profileButton")] [SerializeField] private Button _profileButton;
        [FormerlySerializedAs("helpButton")] [SerializeField] private Button _helpButton;
        [FormerlySerializedAs("settingsButton")] [SerializeField] private Button _settingsButton;
        [FormerlySerializedAs("upperButtonOnMap")] [SerializeField] private Button _upperButtonOnMap;
        [FormerlySerializedAs("lowerButtonOnMap")] [SerializeField] private Button _lowerButtonOnMap;
        [FormerlySerializedAs("exitButton")] [SerializeField] private Button _exitButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("lowerButtonText")] [SerializeField] private TextMeshProUGUI _lowerButtonText;
        [FormerlySerializedAs("upperButtonText")] [SerializeField] private TextMeshProUGUI _upperButtonText;

        private void Awake()
        {
            headerPanel.Initialize("Ekran główny");
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is MainViewInitializationParameters init)
            {
                _profileButton.onClick.AddListener(() => init.ProfileEvent?.Invoke());
                _settingsButton.onClick.AddListener(() => init.SettingsEvent?.Invoke());
                _helpButton.onClick.AddListener(() => init.HelpEvent?.Invoke());
                _upperButtonOnMap.onClick.AddListener(() => init.UpperButtonEvent?.Invoke());
                _lowerButtonOnMap.onClick.AddListener(() => init.LowerButtonEvent?.Invoke());
                _exitButton.onClick.AddListener(() => init.ExitEvent?.Invoke());
                _lowerButtonText.text = init.LowerButtonText;
                _upperButtonText.text = init.UpperButtonText;
            }
        }

        public void OnDisable()
        {
            _profileButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _helpButton.onClick.RemoveAllListeners();
            _upperButtonOnMap.onClick.RemoveAllListeners();
            _lowerButtonOnMap.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}
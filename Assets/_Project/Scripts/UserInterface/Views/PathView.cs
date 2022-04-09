using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a path view. This object can be initialized with PathViewInitializationParameters.
    /// It's extended by IPopupable interface, so it determines the size and position of a popup.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class PathView : MonoBehaviour, IInitializableView, IPopupable
    {
        public RectTransform PopupArea => _popupArea;
        
        [FormerlySerializedAs("endPathButton")] [SerializeField] private Button _endPathButton;
        [FormerlySerializedAs("nextStationInfoButton")] [SerializeField] private Button _nextStationInfoButton;
        [FormerlySerializedAs("helpButton")] [SerializeField] private Button _helpButton;
        [FormerlySerializedAs("mainMenuButton")] [SerializeField] private Button _mainMenuButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("appearanceChangingButton")] [SerializeField] private AppearanceChangingButton _appearanceChangingButton;

        private void Awake()
        {
            _endPathButton.onClick.AddListener(_appearanceChangingButton.SetDefaultAppearance);
            _helpButton.onClick.AddListener(_appearanceChangingButton.SetDefaultAppearance);
            _mainMenuButton.onClick.AddListener(_appearanceChangingButton.SetDefaultAppearance);
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is PathViewInitializationParameters init)
            {
                headerPanel.Initialize(init.HeaderText);
                _endPathButton.onClick.AddListener(() => init.EndPathEvent?.Invoke());
                _helpButton.onClick.AddListener(()=>init.HelpEvent?.Invoke());
                _nextStationInfoButton.onClick.AddListener(()=>init.NextStationInfoEvent.Invoke());
                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
            }
        }

        public void ShowPopup()
        {
            _nextStationInfoButton.onClick?.Invoke();
        }
        
        public void HidePopup()
        {
            _nextStationInfoButton.onClick?.Invoke();
        }
        public void OnDisable()
        {
            _endPathButton.onClick.RemoveAllListeners();
            _helpButton.onClick.RemoveAllListeners();
            _nextStationInfoButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}
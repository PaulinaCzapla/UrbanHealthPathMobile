using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a test view. This object can be initialized with TestViewInitializationParameters.
    /// It's extended by IPopupable interface, so it determines the size and position of a popup.
    /// </summary>
    public class TestView : MonoBehaviour, IInitializableView, IPopupable
    {
        public RectTransform PopupArea => _popupArea;
        
        public UnityAction<float> TimeUpdated { get; set; }

        [SerializeField] private TestButtonGroup _testButtonGroup;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private HeaderPanel _headerPanel;
        [SerializeField] private RectTransform _popupArea;
        [SerializeField] private TextMeshProUGUI _timerText;
        private UnityAction<ChangingButton> _repeatButton;
        private UnityAction<ChangingButton> _timerButton;
        private UnityAction<ChangingButton> _nextButton;

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is TestViewInitializationParameters init)
            {
                _testButtonGroup.Initialize(init.ButtonGroupInitialized);
                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _headerPanel.Initialize(init.HeaderText);
                //init.TimeUpdatedEvent += UpdateTimerText;
                TimeUpdated += UpdateTimerText;
            }
        }
        
        public void OnDisable()
        {
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
        }

        private void UpdateTimerText(float time)
        {
            TimeSpan duration = TimeSpan.FromSeconds(time);
            _timerText.text = duration.ToString(@"hh\:mm\:ss");
        }
    }
}

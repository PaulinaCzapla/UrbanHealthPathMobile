using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a station view. This object can be initialized with StationViewInitializationParameters.
    /// It's extended by IPopupable interface, so it determines the size and position of a popup.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializableView, IPopupable
    {
        public RectTransform PopupArea => _popupArea;

        [SerializeField] private StationButtonGroup _stationButtonGroup;
        
        [FormerlySerializedAs("mainMenuButton")] [SerializeField]
        private Button _mainMenuButton;

        [FormerlySerializedAs("returnButton")] [SerializeField]
        private Button _returnButton;

        [SerializeField] private ButtonWithAudio _buttonWithAudio;

        [SerializeField] private HeaderPanel _headerPanel;
        
        [SerializeField] private RawImage _stationImage;

        [FormerlySerializedAs("popupArea")] [SerializeField]
        private RectTransform _popupArea;
        
        private UnityAction<ChangingButton> _historicButtonAction;
        private UnityAction<ChangingButton> _motorialButtonAction;
        private UnityAction<ChangingButton> _sensoricButtonAction;
        
        private bool _isPlaying = false;
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is StationViewInitializationParameters init)
            {
                _stationButtonGroup.Initialize(init.ButtonGroupInitialized);

                if (init.AudioClip != null)
                {
                    _buttonWithAudio.Initialize(init.AudioClip, init.AudioButtonInitialized);
                    _buttonWithAudio.Button.onClick.AddListener(()=>init.PlayAction?.Invoke(_buttonWithAudio));
                }
                else
                {
                    _buttonWithAudio.Button.interactable = false;
                }

                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _headerPanel.Initialize(init.HeaderText);
                _stationImage.texture = init.StationImage;
            }
        }

        public void OnDisable()
        {
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
            _buttonWithAudio.Button.onClick.RemoveAllListeners();
        }
        
    }
}
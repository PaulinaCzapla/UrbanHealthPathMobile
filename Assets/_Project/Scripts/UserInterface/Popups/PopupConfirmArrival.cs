using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a confirm arrival popup. This object can be initialized with PopupConfirmArrivalInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class PopupConfirmArrival : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("buttonImHere")] [SerializeField] private Button _buttonImHere;
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("fitter")] [SerializeField] private ImageFitter _fitter;
        
        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is PopupConfirmArrivalInitializationParameters init)
            {
                _buttonImHere.onClick.AddListener(init.ButtonAction);
                _text.text = init.Text;
                InitSizeAndPosition(init.Payload);
                _fitter.InitializeImage(init.Texture);
            }
        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }
        
    }
}
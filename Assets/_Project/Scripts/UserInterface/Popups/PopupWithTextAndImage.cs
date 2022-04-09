using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a popup with text and image. This object can be initialized with PopupWithTextAndImageInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class PopupWithTextAndImage : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;
        
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("fitter")] [SerializeField] private ImageFitter _fitter;
        [FormerlySerializedAs("textArea")] [SerializeField] private RectTransform _textArea;
        [FormerlySerializedAs("imageArea")] [SerializeField] private RectTransform _imageArea;

        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is PopupWithTextAndImageInitializationParameters init)
            {
                headerPanel.Initialize(init.HeaderText);
                if (init.Text == String.Empty || init.Text is null)
                {
                    _textArea.gameObject.SetActive(false);
                }
                else
                {
                    _text.text = init.Text;
                }

                if (init.Texture == null)
                {
                    _imageArea.gameObject.SetActive(false);
                }
                else
                {
                    _fitter.InitializeImage(init.Texture);
                }
                InitSizeAndPosition(init.Payload);
            }
        }
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }
    }
}
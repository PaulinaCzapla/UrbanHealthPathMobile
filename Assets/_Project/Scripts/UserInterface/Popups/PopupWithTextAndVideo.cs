using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a popup with text and video. This object can be initialized with PopupWithTextAndVideoInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class PopupWithTextAndVideo : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("videoPlayer")] [SerializeField] private VideoPlayer _videoPlayer;
        
        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is PopupWithTextAndVideoInitializationParameters init)
            {
                headerPanel.Initialize(init.HeaderText);
                _text.text = init.Text;

                _videoPlayer.clip = init.Clip;
                _videoPlayer.Play();

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
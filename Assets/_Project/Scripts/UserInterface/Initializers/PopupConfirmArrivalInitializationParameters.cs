using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PopupConfirmArrival.
    /// </summary>
    public class PopupConfirmArrivalInitializationParameters : IPopupInitializationParameters
    {
        public UnityAction ButtonAction { get; }
        public string Text { get; }
        public Texture2D Texture { get; }
        public PopupPayload Payload { get; }

        public PopupConfirmArrivalInitializationParameters(UnityAction buttonAction, string text,
            Texture2D texture, PopupPayload payload)
        {
            ButtonAction = buttonAction;
            Text = text;
            Texture = texture;
            Payload = payload;
        }
    }
}
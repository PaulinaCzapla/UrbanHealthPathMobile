using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PopupWithTextAndImage.
    /// </summary>
    public class PopupWithTextAndImageInitializationParameters : IPopupInitializationParameters
    {
        public string Text { get; }
        public string HeaderText { get; }
        public Texture2D Texture { get; }
        public PopupPayload Payload { get; }

        public PopupWithTextAndImageInitializationParameters(string headerText, string text, Texture2D texture, PopupPayload payload)
        {
            HeaderText = headerText;
            Text = text;
            Texture = texture;
            Payload = payload;
        }
    }
}
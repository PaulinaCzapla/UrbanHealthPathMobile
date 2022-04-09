using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine.Video;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PopupWithTextAndVideo.
    /// </summary>
    public class PopupWithTextAndVideoInitializationParameters : IPopupInitializationParameters
    {
        public string Text { get; }
        public string HeaderText { get; }
        public VideoClip Clip { get; }
        public PopupPayload Payload { get; }

        public PopupWithTextAndVideoInitializationParameters(string headerText, string text, VideoClip clip, PopupPayload payload)
        {
            HeaderText = headerText;
            Text = text;
            Clip = clip;
            Payload = payload;
        }
    }
}
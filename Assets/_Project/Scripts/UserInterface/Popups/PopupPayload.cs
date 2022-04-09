using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that determines popup's size and position.
    /// </summary>
    public class PopupPayload
    {
        public Vector2 Position { get; }
        public Vector2 Size { get; }

        public PopupPayload(RectTransform rectTransform)
        {
            Position = rectTransform.transform.position;
            Size = rectTransform.sizeDelta;
        }
    }
}
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    /// <summary>
    /// Interface that determines a view that can display popups. It contains a PopupArea property,
    /// that determines size and position of a popup.
    /// </summary>
    public interface IPopupable
    {
        public RectTransform PopupArea { get; }
    }
}
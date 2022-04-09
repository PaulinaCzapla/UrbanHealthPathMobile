using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    /// <summary>
    /// Interface that determines a popup. It contains PopupArea property and a method for
    /// popup size and position initialization.
    /// </summary>
    public interface IPopup
    {
        public RectTransform PopupArea { get;  }
        public void InitSizeAndPosition(PopupPayload payload);
    }
}
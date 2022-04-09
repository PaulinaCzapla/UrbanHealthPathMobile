using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    /// <summary>
    /// Interface that determines a popup that can be initialized.
    /// </summary>
    public interface IInitializablePopup
    {
        public void Initialize(IPopupInitializationParameters initializationParameters);
    }
}
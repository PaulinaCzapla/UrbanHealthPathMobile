using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Interfaces
{
    /// <summary>
    /// Interface that determines a view that can be initialized.
    /// </summary>
    public interface IInitializableView
    {
        public void Initialize(IViewInitializationParameters initializationParameters);
    }
}

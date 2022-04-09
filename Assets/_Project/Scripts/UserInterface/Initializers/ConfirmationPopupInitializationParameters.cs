using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for ConfirmationPopup.
    /// </summary>
    public class ConfirmationPopupInitializationParameters : IPopupInitializationParameters
    {
        public string Information { get; }
        public int NumberOfButtons { get; }
        public string[] ButtonTexts { get; }
        public UnityAction[] Actions { get; }

        public ConfirmationPopupInitializationParameters(string information, int numberOfButtons = 0, string[] buttonTexts = null,
            UnityAction[] actions = null)
        {
            Information = information;
            NumberOfButtons = Mathf.Clamp(numberOfButtons, 0, 2);
            ButtonTexts = buttonTexts;
            Actions = actions;
        }
    }
}
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for QuizWithTextPopup.
    /// </summary>
    public class QuizWithTextPopupInitializationParameters : IPopupInitializationParameters
    {
        public string Question { get;  }
        public PopupPayload Payload { get;  }
        public QuizWithTextElementOption[] Options { get; }
        
        public QuizWithTextPopupInitializationParameters(string question, PopupPayload payload, QuizWithTextElementOption[] options)
        {
            Question = question;
            Payload = payload;
            Options = options;
        }
    }
}
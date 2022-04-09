using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for QuizWithImagesPopup.
    /// </summary>
    public class QuizWithImagesPopupInitializationParameters : IPopupInitializationParameters
    {
        public string Question { get;  }
        
        public PopupPayload Payload { get;  }

        public QuizElementOption[] QuizElementOptions { get; }
        
        public QuizWithImagesPopupInitializationParameters(string question, PopupPayload payload, QuizElementOption[] quizElementOptions)
        {
            Question = question;
            Payload = payload;
            QuizElementOptions = quizElementOptions;
        }

    }
}
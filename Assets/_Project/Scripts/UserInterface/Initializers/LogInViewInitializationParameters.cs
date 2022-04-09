using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for LogInView.
    /// </summary>
    public class LogInViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction LogInEvent { get; }
        public UnityAction ContinueWithoutLogInEvent { get; }

        public LogInViewInitializationParameters(UnityAction logInEvent, UnityAction continueWithoutLogInEvent)
        {
            LogInEvent += logInEvent;
            ContinueWithoutLogInEvent += continueWithoutLogInEvent;
        }
    }
}
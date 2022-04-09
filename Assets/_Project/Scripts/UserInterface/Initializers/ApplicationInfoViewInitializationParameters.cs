using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for ApplicationInfoView.
    /// </summary>
    public class ApplicationInfoViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction LeftButton { get; }
        public UnityAction RightButton { get; }

        public ApplicationInfoViewInitializationParameters(UnityAction leftButton, UnityAction rightButton)
        {
            LeftButton += leftButton;
            RightButton += rightButton;
        }
    }
}
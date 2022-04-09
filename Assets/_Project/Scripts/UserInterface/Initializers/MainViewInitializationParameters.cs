using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for MainView.
    /// </summary>
    public class MainViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction ProfileEvent { get; }
        public UnityAction HelpEvent { get; }
        public UnityAction SettingsEvent { get; }
        public UnityAction LowerButtonEvent { get; }
        public UnityAction UpperButtonEvent { get; }
        public UnityAction ExitEvent { get; }
        
        public string UpperButtonText { get;  }
        public string LowerButtonText { get; }

        public MainViewInitializationParameters(UnityAction profileEvent, UnityAction helpEvent, UnityAction settingsEvent,
            UnityAction upperButtonEvent, UnityAction lowerButtonEvent, UnityAction exitEvent, string upperButtonText, string lowerButtonText)
        {
            ProfileEvent += profileEvent;
            HelpEvent += helpEvent;
            SettingsEvent += settingsEvent;
            LowerButtonEvent += lowerButtonEvent;
            UpperButtonEvent += upperButtonEvent;
            ExitEvent += exitEvent;
            LowerButtonText = lowerButtonText;
            UpperButtonText = upperButtonText;
        }
    }
}
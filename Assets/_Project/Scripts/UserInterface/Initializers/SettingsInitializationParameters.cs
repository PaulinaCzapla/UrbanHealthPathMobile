using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for SettingsView.
    /// </summary>
    public class SettingsInitializationParameters : IViewInitializationParameters
    {
        public string HeaderText { get;}
        public UnityAction ReturnEvent { get; }
        public UnityAction RevertEvent { get; }
        public UnityAction FontEvent { get; }
        public UnityAction ThemeEvent { get; }
        public UnityAction AudioEvent { get; }

        public SettingsInitializationParameters(UnityAction returnEvent, UnityAction revertEvent, UnityAction
            fontEvent, UnityAction themeEvent, UnityAction audioEvent,string headerText = "Ustawienia")
        {
            HeaderText = headerText;
            ReturnEvent += returnEvent;
            RevertEvent += revertEvent;
            FontEvent += fontEvent;
            ThemeEvent += themeEvent;
            AudioEvent += audioEvent;
        }
    }
}
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PathView.
    /// </summary>
    public class PathViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction EndPathEvent { get; }
        public UnityAction NextStationInfoEvent { get; }
        public UnityAction HelpEvent { get; }
        public UnityAction MainMenuEvent { get; }
        public string HeaderText { get; }
        public PathViewInitializationParameters(UnityAction endPathEvent, UnityAction nextStationInfoEvent, UnityAction helpEvent,
            UnityAction mainMenuEvent, string headerText)
        {
            EndPathEvent += endPathEvent;
            NextStationInfoEvent += nextStationInfoEvent;
            HelpEvent += helpEvent;
            MainMenuEvent += mainMenuEvent;
            HeaderText = headerText;
        }
    }
}
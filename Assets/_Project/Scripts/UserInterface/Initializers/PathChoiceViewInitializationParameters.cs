using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PathChoiceView.
    /// </summary>
    public class PathChoiceViewInitializationParameters : IViewInitializationParameters
    {
        public List<ListElement> Elements { get; }
        public UnityAction MainMenuEvent { get; }

        public PathChoiceViewInitializationParameters(List<ListElement> elements, UnityAction mainMenuEvent)
        {
            Elements = elements;
            MainMenuEvent += mainMenuEvent;
        }
    }
}
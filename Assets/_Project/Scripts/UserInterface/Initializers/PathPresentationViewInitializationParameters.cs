using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PathPresentationView.
    /// </summary>
    public class PathPresentationViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public UnityAction ChooseButtonAction { get; }
        public string HeaderText { get; }
        public int StationCount { get; }
        public double PathLength { get; }
        public Texture MapTexture { get; }

        public PathPresentationViewInitializationParameters(UnityAction mainMenuEvent, UnityAction returnEvent,
            UnityAction chooseButtonAction, string headerText, int stationCount, 
            int pathLength, Texture mapTexture)
        {
            MainMenuEvent = mainMenuEvent;
            ReturnEvent = returnEvent;
            ChooseButtonAction = chooseButtonAction;
            HeaderText = headerText;
            StationCount = stationCount;
            PathLength = pathLength;
            MapTexture = mapTexture;
        }
    }
}

using PolSl.UrbanHealthPath.UserInterface.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for StationView.
    /// </summary>
    public class StationViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction<StationButtonGroup> ButtonGroupInitialized { get; }
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public UnityAction<ButtonWithAudio> AudioButtonInitialized { get; }
        public UnityAction<ButtonWithAudio> PlayAction { get; }
        public AudioClip AudioClip { get; }
        public string HeaderText { get; }
        public Texture2D StationImage { get; }
        
        public StationViewInitializationParameters(UnityAction<StationButtonGroup> buttonGroupInitialized, UnityAction mainMenuEvent, UnityAction returnEvent,
            string headerText, Texture2D stationImage, UnityAction<ButtonWithAudio> audioButtonInitialized, UnityAction<ButtonWithAudio> playAction, AudioClip audioClip)
        {
            ButtonGroupInitialized += buttonGroupInitialized;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            HeaderText = headerText;
            StationImage = stationImage;
            PlayAction = playAction;
            AudioClip = audioClip;
            AudioButtonInitialized = audioButtonInitialized;
        }
    }
}
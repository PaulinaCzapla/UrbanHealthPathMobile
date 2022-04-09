using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for TestView.
    /// </summary>
    public class TestViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction<TestButtonGroup> ButtonGroupInitialized { get; }
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public event UnityAction<float> TimeUpdatedEvent;
        public string HeaderText { get; }

        public TestViewInitializationParameters(UnityAction<TestButtonGroup> buttonGroupInitialized,
            UnityAction mainMenuEvent, UnityAction returnEvent, UnityAction<float> timeUpdatedEvent, string headerText)
        {
            ButtonGroupInitialized += buttonGroupInitialized;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            TimeUpdatedEvent += timeUpdatedEvent;
            HeaderText = headerText;
        }
    }
}

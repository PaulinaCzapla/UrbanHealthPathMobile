using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Group of buttons used in the Test View.
    /// </summary>
    public class TestButtonGroup : MonoBehaviour
    {
        [SerializeField] private ChangingButton _repeatButton;
        [SerializeField] private ChangingButton _timerButton;
        [SerializeField] private ChangingButton _nextButton;

        private Action _unregisterFromPopupAndViewEvents;
        private Action _unregisterFromTestEvents;

        public ChangingButton RepeatButton => _repeatButton;
        public ChangingButton TimerButton => _timerButton;
        public ChangingButton NextButton => _nextButton;
        
        public void Initialize(UnityAction<TestButtonGroup> initialized)
        {
            initialized?.Invoke(this);
        }

        private void OnDisable()
        {
            RemoveAllListeners();
            _unregisterFromTestEvents?.Invoke();
        }

        private void RemoveAllListeners()
        {
            _nextButton.Button.onClick.RemoveAllListeners();
            _timerButton.Button.onClick.RemoveAllListeners();
            _repeatButton.Button.onClick.RemoveAllListeners();
        }

        public void AddListenerToRepeatButton(UnityAction action)
        {
            _repeatButton.Button.onClick.AddListener(action);
        }

        public void AddListenerToTimerButton(UnityAction action)
        {
            _timerButton.Button.onClick.AddListener(action);
        }

        public void AddListenerToNextButton(UnityAction action)
        {
            _nextButton.Button.onClick.AddListener(action);
        }
    }
}

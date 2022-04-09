using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a test introduction view. This object can be initialized with TestIntroductionInitializationParameters.
    /// </summary>
    public class TestIntroductionView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _mainWindowButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _startTextButton;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is TestIntroductionInitializationParameters init)
            {
                _mainWindowButton.onClick.AddListener(() => init.MainWindowAction?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnAction?.Invoke());
                _startTextButton.onClick.AddListener(()=>init.StartTestAction.Invoke());
            }
        }

        public void OnDisable()
        {
            _startTextButton.onClick.RemoveAllListeners();
            _mainWindowButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}

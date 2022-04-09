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
    /// A class that represents a test summary view. This object can be initialized with TestSummaryInitializationParameters.
    /// </summary>
    public class TestSummaryView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _finishTestButton;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is TestSummaryInitializationParameters init)
            {
                _finishTestButton.onClick.AddListener(() => init.FinishTestAction?.Invoke());
            }
        }

        public void OnDisable()
        {
            _finishTestButton.onClick.RemoveAllListeners();
        }
    }
}

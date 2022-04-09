using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a log in view. This object can be initialized with LogInViewInitializationParameters.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class LoginInView : MonoBehaviour, IDisplayable, IInitializableView
    {
        [FormerlySerializedAs("loginButton")] [SerializeField] private Button _loginButton;
        [FormerlySerializedAs("continueWithoutLoginButton")] [SerializeField] private Button _continueWithoutLoginButton;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is LogInViewInitializationParameters init)
            {
                _loginButton.onClick.AddListener(() => init.LogInEvent?.Invoke());
                _continueWithoutLoginButton.onClick.AddListener(() => init.ContinueWithoutLogInEvent?.Invoke());
            }
        }

        public void Display()
        {
        }

        public void Hide()
        {
        }

        private void OnDisable()
        {
            _loginButton.onClick.RemoveAllListeners();
            _continueWithoutLoginButton.onClick.RemoveAllListeners();
        }
    }
}
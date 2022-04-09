using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents an icon info view. This object can be initialized with ApplicationInfoViewInitializationParameters.
    /// </summary>
    public class IconInfoView : MonoBehaviour, IDisplayable, IInitializableView
    {
        [FormerlySerializedAs("backButton")] [SerializeField] private Button _backButton;
        [FormerlySerializedAs("forwardButton")] [SerializeField] private Button _forwardButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        
        private void Awake()
        {
            headerPanel.Initialize("Wyjaśnienie ikon");
        }
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is ApplicationInfoViewInitializationParameters init)
            {
                _backButton.onClick.AddListener(() => init.LeftButton?.Invoke());
                _forwardButton.onClick.AddListener(() => init.RightButton?.Invoke());
            }
        }
        public void Display()
        {
        }

        public void Hide()
        {
        }
        
        public void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
            _forwardButton.onClick.RemoveAllListeners();
        }
    }
}

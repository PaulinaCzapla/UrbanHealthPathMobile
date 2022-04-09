using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents an exercise icon info view. This object can be initialized with ApplicationInfoViewInitializationParameters.
    /// </summary>
    public class ExerciseIconInfoView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _resturnButton;
        [SerializeField] private HeaderPanel _headerPanel;
        
        private void Awake()
        {
            _headerPanel.Initialize("Wyjaśnienie ikon");
        }
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is ApplicationInfoViewInitializationParameters init)
            {
                _menuButton.onClick.AddListener(() => init.LeftButton?.Invoke());
                _resturnButton.onClick.AddListener(() => init.RightButton?.Invoke());
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
            _menuButton.onClick.RemoveAllListeners();
            _resturnButton.onClick.RemoveAllListeners();
        }
    }
}
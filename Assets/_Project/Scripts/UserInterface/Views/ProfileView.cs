using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a profile view. This object can be initialized with ProfileViewInitializationParameters.
    /// </summary>
    public class ProfileView : MonoBehaviour, IInitializableView, IDisplayable
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private HeaderPanel _headerPanel;
        [SerializeField] private ListPanel _list;
        private void Awake()
        {
            _headerPanel.Initialize("Twój profil");
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is ProfileViewInitializationParameters init)
            {
                _list.Initialize(init.ListElements);
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _headerPanel.Initialize(init.Header);
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
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}
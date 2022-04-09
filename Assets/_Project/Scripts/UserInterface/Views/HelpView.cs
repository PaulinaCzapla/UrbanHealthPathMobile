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
    /// A class that represents a help view. This object can be initialized with HelpViewInitializationParameters.
    /// </summary>
    public class HelpView : MonoBehaviour, IDisplayable, IInitializableView
    {
        [FormerlySerializedAs("returnButton")] [SerializeField] private Button _returnButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("list")] [SerializeField] private ListPanel _list;

        private void Awake()
        {
            headerPanel.Initialize("Pomoc");
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is HelpViewInitializationParameters init)
            {
                _list.Initialize(init.Elements);
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
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
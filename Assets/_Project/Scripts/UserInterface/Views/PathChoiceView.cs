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
    /// A class that represents a path choice view. This object can be initialized with PathChoiceViewInitializationParameter.
    /// </summary>
    public class PathChoiceView : MonoBehaviour, IDisplayable, IInitializableView
    {
        [FormerlySerializedAs("menuButton")] [SerializeField] private Button _menuButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;
        [FormerlySerializedAs("list")] [SerializeField] private ListPanel _list;
        private void Awake()
        {
            headerPanel.Initialize("Wybór ścieżki");
        }
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is PathChoiceViewInitializationParameters init)
            {
                _list.Initialize(init.Elements);
                _menuButton.onClick.AddListener(()=>init.MainMenuEvent?.Invoke());
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
        }
    }
}
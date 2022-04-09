using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a path presentation view. This object can be initialized with PathPresentationViewInitializationParameter.
    /// </summary>
    public class PathPresentationView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _chooseButton;
        [SerializeField] private HeaderPanel _headerPanel;
        [SerializeField] private TextMeshProUGUI _stationCount;
        [SerializeField] private TextMeshProUGUI _distance;
        [SerializeField] private RawImage _mapImage;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is PathPresentationViewInitializationParameters init)
            {
                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _chooseButton.onClick.AddListener(() => init.ChooseButtonAction?.Invoke());
                _headerPanel.Initialize(init.HeaderText);
                _stationCount.text = "Liczba punktów do odwiedzenia: " + init.StationCount;
                _distance.text = $"Dystans możliwy do przebycia: {init.PathLength} m";
                _mapImage.texture = init.MapTexture;
            }
        }
        
        public void OnDisable()
        {
            _chooseButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}

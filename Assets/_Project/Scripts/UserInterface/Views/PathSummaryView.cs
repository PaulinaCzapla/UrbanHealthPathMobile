using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// A class that represents a path summary view. This object can be initialized with PathSummaryViewInitializationParameters.
    /// </summary>
    public class PathSummaryView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _finishButton;
        [SerializeField] private Button _shareButton;
        [SerializeField] private TextMeshProUGUI _pointsVisitedText;
        [SerializeField] private TextMeshProUGUI _distanceText;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is PathSummaryViewInitializationParameters init)
            {
                _finishButton.onClick.AddListener(() => init.FinishButtonEvent?.Invoke());
                _shareButton.onClick.AddListener(() => init.ShareButtonEvent?.Invoke());
                _pointsVisitedText.text = Convert.ToString(init.PointsVisited);
                _distanceText.text = $"{init.Distance} m";
            }
        }
        
        public void OnDisable()
        {
            _finishButton.onClick.RemoveAllListeners();
            _shareButton.onClick.RemoveAllListeners();
        }
    }
}

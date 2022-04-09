using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A class that represents a statistics view. This object can be initialized with StatisticsViewInitializationParameters.
    /// </summary>
    public class StatisticsView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _mainViewButton;

        [SerializeField] private Button _returnButton;

        [SerializeField] private Button _shareButton;

        [SerializeField] private TextMeshProUGUI _pathsFinished;

        [SerializeField] private TextMeshProUGUI _pointsVisited;
        
        [SerializeField] private TextMeshProUGUI _exercisesFinished;
        
        [SerializeField] private TextMeshProUGUI _distance;
        
        [SerializeField] private TextMeshProUGUI _timeSpent;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is StatisticsViewInitializationParameters init)
            {
                _mainViewButton.onClick.AddListener(() => init.MainViewButtonEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnButtonEvent?.Invoke());
                _shareButton.onClick.AddListener(() => init.ShareButtonEvent?.Invoke());
                _pathsFinished.text = init.PathsFinished.ToString();
                _pointsVisited.text = init.PointsVisited.ToString();
                _exercisesFinished.text = init.ExercisesFinished.ToString();
                _distance.text = init.Distance.ToString();
                _timeSpent.text = ConvertTimeToString(init.TimeSpent);
            }
        }

        private string ConvertTimeToString(int time)
        {
            if (time < 60)
            {
                return time.ToString() + "m";
            }
            else
            {
                string hoursString = (time / 60).ToString() + "h";
                int minutes = time % 60;
                string minutesString;
                if (minutes >= 10)
                {
                    minutesString = minutes.ToString() + "m";
                }
                else
                {
                    minutesString = "0" + minutes.ToString() + "m";
                }

                return hoursString + minutesString;
            }
        }
        
        public void OnDisable()
        {
            _mainViewButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
            _shareButton.onClick.RemoveAllListeners();
        }
    }
    
    
    
}

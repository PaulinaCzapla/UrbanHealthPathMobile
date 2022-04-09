using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for StatisticsView.
    /// </summary>
    public class StatisticsViewInitializationParameters
    {
        public UnityAction MainViewButtonEvent { get; }
        
        public UnityAction ReturnButtonEvent { get; }
        
        public UnityAction ShareButtonEvent { get; }

        public int PathsFinished { get; }
        
        public int PointsVisited { get; }
        
        public int ExercisesFinished { get; }
        
        public int Distance { get; }
        
        public int TimeSpent { get; }

        public StatisticsViewInitializationParameters(UnityAction mainViewButtonEvent, UnityAction returnButtonEvent,
            UnityAction shareButtonEvent, int pathsFinished, int pointsVisited, int exercisesFinished, int distance, 
            int timeSpent)
        {
            MainViewButtonEvent = mainViewButtonEvent;
            ReturnButtonEvent = returnButtonEvent;
            ShareButtonEvent = shareButtonEvent;
            PathsFinished = pathsFinished;
            PointsVisited = pointsVisited;
            ExercisesFinished = exercisesFinished;
            Distance = distance;
            TimeSpent = timeSpent;
        }
    }
}

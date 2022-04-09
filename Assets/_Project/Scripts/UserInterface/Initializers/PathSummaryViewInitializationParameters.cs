

using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for PathSummaryView.
    /// </summary>
    public class PathSummaryViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction FinishButtonEvent { get; }
        
        public UnityAction ShareButtonEvent { get; }
        
        public int PointsVisited { get; }
        
        public int Distance { get; }

        public PathSummaryViewInitializationParameters(UnityAction finishButtonEvent, UnityAction shareButtonEvent,
            int pointsVisited, int distance)
        {
            FinishButtonEvent = finishButtonEvent;
            ShareButtonEvent = shareButtonEvent;
            PointsVisited = pointsVisited;
            Distance = distance;
        }
    }
}

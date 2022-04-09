using System.Collections.Generic;
using System.Linq;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds details of an urban path.
    /// </summary>
    public class UrbanPath
    {
        public string PathId { get; }
        public string DisplayedName { get; }
        public int ApproximateDistanceInMeters { get; }
        public bool IsCyclic { get; }
        public IList<LateBoundValue<Waypoint>> Waypoints { get; }
        public string MapUrl { get; }
        public LateBoundValue<MediaFile> PreviewImage { get; }
        public LateBoundValue<MediaFile> Icon { get; }

        public UrbanPath(string pathId, string displayedName, int approximateDistanceInMeters, bool isCyclic,
            string mapUrl, IList<LateBoundValue<Waypoint>> waypoints, LateBoundValue<MediaFile> previewImage,
            LateBoundValue<MediaFile> icon)
        {
            PathId = pathId;
            DisplayedName = displayedName;
            ApproximateDistanceInMeters = approximateDistanceInMeters;
            IsCyclic = isCyclic;
            MapUrl = mapUrl;
            Waypoints = waypoints;
            PreviewImage = previewImage;
            Icon = icon;
        }

        public IList<T> GetWaypointsOfType<T>() where T : Waypoint
        {
            return Waypoints.Where(x => x.Value is T).Select(x => (T) x.Value).ToList();
        }
    }
}
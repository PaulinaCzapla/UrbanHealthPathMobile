using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Loader of waypoints from JSON format.
    /// </summary>
    public class JsonWaypointsLoader : JsonDataLoader<Waypoint>, IWaypointsLoader
    {
        public JsonWaypointsLoader(JToken json, IParser<JObject, Waypoint> waypointsParser) : base(json ,waypointsParser)
        {
        }

        public IList<Waypoint> LoadWaypoints()
        {
            return LoadJsonData();
        }
    }
}
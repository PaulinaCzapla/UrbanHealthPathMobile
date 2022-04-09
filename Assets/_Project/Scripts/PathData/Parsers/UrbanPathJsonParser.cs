using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into UrbanPath.
    /// </summary>
    public class UrbanPathJsonParser : ValidatedJsonObjectParser<UrbanPath>
    {
        private const string ID_KEY = "path_id";
        private const string DISPLAYED_NAME_KEY = "displayed_name";
        private const string APPROXIMATE_DISTANCE_KEY = "approximate_distance_in_meters";
        private const string IS_CYCLIC_KEY = "is_cyclic";
        private const string MAP_URL_KEY = "map_url";
        private const string WAYPOINTS_KEY = "waypoints";
        private const string PREVIEW_IMAGE_KEY = "preview_image";
        private const string ICON_KEY = "icon";

        public UrbanPathJsonParser() : base(new []{ID_KEY, DISPLAYED_NAME_KEY, APPROXIMATE_DISTANCE_KEY,
            IS_CYCLIC_KEY, MAP_URL_KEY, WAYPOINTS_KEY, PREVIEW_IMAGE_KEY})
        {
        }

        protected override UrbanPath ParseJsonObject(JObject json)
        {
            List<LateBoundValue<Waypoint>> waypoints = new List<LateBoundValue<Waypoint>>();

            foreach (JToken waypoint in json[WAYPOINTS_KEY])
            {
                waypoints.Add(new LateBoundValue<Waypoint>(waypoint.Value<string>()));
            }
            
            LateBoundValue<MediaFile> previewImage =
                new LateBoundValue<MediaFile>(json[PREVIEW_IMAGE_KEY].Value<string>());

            LateBoundValue<MediaFile> icon = new LateBoundValue<MediaFile>(json[ICON_KEY].Value<string>());

            return new UrbanPath(json[ID_KEY].Value<string>(), json[DISPLAYED_NAME_KEY].Value<string>(),
                json[APPROXIMATE_DISTANCE_KEY].Value<int>(), json[IS_CYCLIC_KEY].Value<bool>(),
                json[MAP_URL_KEY].Value<string>(), waypoints, previewImage, icon);
        }
    }
}
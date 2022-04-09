using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into Waypoint.
    /// </summary>
    public class WaypointJsonParser : ValidatedJsonObjectParser<Waypoint>
    {
        private const string TYPE_KEY = "type";

        private readonly IDictionary<string, IParser<JObject, Waypoint>> _registeredTypesParsers;

        public WaypointJsonParser(JsonObjectParser<Station> stationParser) : base(new[] {TYPE_KEY})
        {
            _registeredTypesParsers = new Dictionary<string, IParser<JObject, Waypoint>>();
            _registeredTypesParsers.Add("station", stationParser);
        }
        
        protected WaypointJsonParser(string[] requiredKeys) : base(requiredKeys)
        {
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            if (!_registeredTypesParsers.ContainsKey(json[TYPE_KEY].Value<string>()))
            {
                throw new ParsingException();
            }
        }

        protected override Waypoint ParseJsonObject(JObject json)
        {
            return _registeredTypesParsers[json[TYPE_KEY].Value<string>()].Parse(json);
        }
    }
}
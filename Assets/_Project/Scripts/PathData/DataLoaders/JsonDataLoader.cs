using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Base class for all loaders that use data in JSON format as their input.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JsonDataLoader<T>
    {
        private readonly JToken _json;
        private readonly IParser<JObject, T> _parser;

        protected JsonDataLoader(JToken json, IParser<JObject, T> parser)
        {
            _json = json;
            _parser = parser;
        }

        protected IList<T> LoadJsonData()
        {
            return _json.Select(data => _parser.Parse((JObject) data)).ToList();
        }
    }
}
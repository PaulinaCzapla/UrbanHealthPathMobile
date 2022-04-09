using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Loader of exercises from JSON format.
    /// </summary>
    public class JsonExercisesLoader : JsonDataLoader<Exercise>, IExercisesLoader
    {
        public JsonExercisesLoader(JToken json, IParser<JObject, Exercise> exerciseParser) : base(json, exerciseParser)
        {
        }

        public IList<Exercise> LoadExercises()
        {
            return LoadJsonData();
        }
    }
}
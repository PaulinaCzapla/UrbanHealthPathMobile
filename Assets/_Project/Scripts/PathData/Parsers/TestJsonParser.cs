using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;
using Random = System.Random;

namespace PolSl.UrbanHealthPath
{
    public class TestJsonParser : ValidatedJsonObjectParser<Test>
    {
        private const string ID_KEY = "test_id";
        private const string EXERCISES_KEY = "exercises";
        
        public TestJsonParser() : base(new[]
        {
            ID_KEY, EXERCISES_KEY
        })
        {}
        
        protected override Test ParseJsonObject(JObject json)
        {
            List<LateBoundValue<Exercise>> exercises = ParseExercises(json);
            return new Test(json[ID_KEY].Value<string>(), exercises);
        }
        
        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            if (json[EXERCISES_KEY].Type != JTokenType.Array)
            {
                throw new ParsingException();
            }
        }
        
        private List<LateBoundValue<Exercise>> ParseExercises(JObject json)
        {
            List<LateBoundValue<Exercise>> exercises = new List<LateBoundValue<Exercise>>();

            JArray jsonExercises = (JArray) json[EXERCISES_KEY];

            if (jsonExercises.HasValues)
            {
                JToken exerciseGroup = GetExerciseGroupToAdd(jsonExercises);

                foreach (JToken exercise in exerciseGroup)
                {
                    exercises.Add(new LateBoundValue<Exercise>((string) exercise));
                }
            }

            return exercises;
        }
        
        private JToken GetExerciseGroupToAdd(JArray jsonExercises)
        {
            bool hasExerciseGroups = jsonExercises.Any(x => x.Type == JTokenType.Array);

            return hasExerciseGroups ? jsonExercises[new Random().Next(0, jsonExercises.Count)] : jsonExercises;
        }
    }
}

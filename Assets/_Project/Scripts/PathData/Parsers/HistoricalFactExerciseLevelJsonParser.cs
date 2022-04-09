using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into HistoricalFactExerciseLevel.
    /// </summary>
    public class HistoricalFactExerciseLevelJsonParser : ValidatedJsonObjectParser<HistoricalFactExerciseLevel>
    {
        private const string FACT_KEY = "fact";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";

        public HistoricalFactExerciseLevelJsonParser() : base(new[] {FACT_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY})
        {
        }
        
        protected override HistoricalFactExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());

            return new HistoricalFactExerciseLevel(difficultyRange, json[FACT_KEY].Value<string>());
        }
    }
}
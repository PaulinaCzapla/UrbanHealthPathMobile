using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into TextExerciseLevel.
    /// </summary>
    public class TextExerciseLevelJsonParser : ValidatedJsonObjectParser<TextExerciseLevel>
    {
        private const string DESCRIPTION_KEY = "description";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";

        public TextExerciseLevelJsonParser() : base(new[] {DESCRIPTION_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY})
        {
        }
        
        protected override TextExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());

            return new TextExerciseLevel(difficultyRange, json[DESCRIPTION_KEY].Value<string>());
        }
    }
}
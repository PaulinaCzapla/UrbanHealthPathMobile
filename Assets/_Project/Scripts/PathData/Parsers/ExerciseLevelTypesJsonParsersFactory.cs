using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Factory of JSON parsers of exercise level types.
    /// </summary>
    public class ExerciseLevelTypesJsonParsersFactory : IExerciseLevelTypesParsersFactory<JObject>
    {
        public IParser<JObject, TextExerciseLevel> CreateTextExerciseParser()
        {
            return new TextExerciseLevelJsonParser();
        }

        public IParser<JObject, VideoExerciseLevel> CreateVideoExerciseParser()
        {
            return new VideoExerciseLevelJsonParser();
        }

        public IParser<JObject, ImageExerciseLevel> CreateImageExerciseParser()
        {
            return new ImageExerciseLevelJsonParser();
        }

        public IParser<JObject, AnswerSelectionExerciseLevel> CreateAnswerSelectionExerciseParser()
        {
            return new AnswerSelectionExerciseLevelJsonParser();
        }

        public IParser<JObject, ImageSelectionExerciseLevel> CreateImageSelectionExerciseParser()
        {
            return new ImageSelectionExerciseLevelJsonParser();
        }

        public IParser<JObject, HistoricalFactExerciseLevel> CreateHistoricalFactExerciseParser()
        {
            return new HistoricalFactExerciseLevelJsonParser();
        }

        public IParser<JObject, ImageSelectionExplanationExerciseLevel> CreateImageSelectionExplanationExerciseParser()
        {
            return new ImageSelectionExplanationExerciseLevelJsonParser();
        }
    }
}
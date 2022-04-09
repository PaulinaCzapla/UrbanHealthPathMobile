using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into ImageSelectionExerciseLevel.
    /// </summary>
    public class ImageSelectionExerciseLevelJsonParser : ValidatedJsonObjectParser<ImageSelectionExerciseLevel>
    {
        private const string QUESTION_KEY = "question";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";
        private const string IMAGES_KEY = "images";
        private const string CORRECT_ANSWERS_KEY = "correct_answers";

        public ImageSelectionExerciseLevelJsonParser() : base(new[] {QUESTION_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY, IMAGES_KEY, CORRECT_ANSWERS_KEY})
        {
        }
        
        protected override ImageSelectionExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());
            
            List<LateBoundValue<MediaFile>> images = new List<LateBoundValue<MediaFile>>();
            
            foreach (JToken image in json[IMAGES_KEY])
            {
                images.Add(new LateBoundValue<MediaFile>(image.Value<string>()));
            }

            List<int> correctAnswers = new List<int>();

            foreach (JToken correctAnswerIndex in json[CORRECT_ANSWERS_KEY])
            {
                correctAnswers.Add(correctAnswerIndex.Value<int>());
            }

            return new ImageSelectionExerciseLevel(difficultyRange, json[QUESTION_KEY].Value<string>(), images, correctAnswers);
        }
    }
}
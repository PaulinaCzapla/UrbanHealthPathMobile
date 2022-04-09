using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into ImageSelectionExplanationExerciseLevel.
    /// </summary>
    public class ImageSelectionExplanationExerciseLevelJsonParser : ValidatedJsonObjectParser<ImageSelectionExplanationExerciseLevel>
    {
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";
        private const string IMAGES_KEY = "images";
        private const string EXPLANATIONS_KEY = "explanations";

        public ImageSelectionExplanationExerciseLevelJsonParser() : base(new[] {MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY, IMAGES_KEY, EXPLANATIONS_KEY})
        {
        }
        
        protected override ImageSelectionExplanationExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());
            
            List<LateBoundValue<MediaFile>> images = new List<LateBoundValue<MediaFile>>();
            
            foreach (JToken image in json[IMAGES_KEY])
            {
                images.Add(new LateBoundValue<MediaFile>(image.Value<string>()));
            }

            List<string> explanations = new List<string>();

            foreach (JToken explanation in json[EXPLANATIONS_KEY])
            {
                explanations.Add(explanation.Value<string>());
            }

            return new ImageSelectionExplanationExerciseLevel(difficultyRange, images, explanations);
        }
    }
}
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into ImageExerciseLevel.
    /// </summary>
    public class ImageExerciseLevelJsonParser : ValidatedJsonObjectParser<ImageExerciseLevel>
    {
        private const string DESCRIPTION_KEY = "description";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";
        private const string IMAGE_FILE_KEY = "image_file";

        public ImageExerciseLevelJsonParser() : base(new[] {DESCRIPTION_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY, IMAGE_FILE_KEY})
        {
        }
        
        protected override ImageExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());
            LateBoundValue<MediaFile> imageFile =
                new LateBoundValue<MediaFile>(json[IMAGE_FILE_KEY].Value<string>());

            return new ImageExerciseLevel(difficultyRange, json[DESCRIPTION_KEY].Value<string>(), imageFile);
        }
    }
}
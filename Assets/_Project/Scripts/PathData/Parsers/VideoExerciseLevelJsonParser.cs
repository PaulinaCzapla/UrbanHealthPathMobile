using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into VideoExercise.
    /// </summary>
    public class VideoExerciseLevelJsonParser : ValidatedJsonObjectParser<VideoExerciseLevel>
    {
        private const string DESCRIPTION_KEY = "description";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";
        private const string VIDEO_FILE_KEY = "video_file";

        public VideoExerciseLevelJsonParser() : base(new[] {DESCRIPTION_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY, VIDEO_FILE_KEY})
        {
        }
        
        protected override VideoExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());
            LateBoundValue<MediaFile> videoFile =
                new LateBoundValue<MediaFile>(json[VIDEO_FILE_KEY].Value<string>());

            return new VideoExerciseLevel(difficultyRange, json[DESCRIPTION_KEY].Value<string>(), videoFile);
        }
    }
}
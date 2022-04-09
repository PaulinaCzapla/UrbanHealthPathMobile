namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Exercise level that consists of text description and video file.
    /// </summary>
    public class VideoExerciseLevel : ExerciseLevel
    {
        public string Description { get; }
        public LateBoundValue<MediaFile> VideoFile { get; }

        public VideoExerciseLevel(DifficultyRange difficultyRange, string description, LateBoundValue<MediaFile> videoFile) : base(difficultyRange)
        {
            Description = description;
            VideoFile = videoFile;
        }
    }
}
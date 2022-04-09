namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Exercise level that contains only text.
    /// </summary>
    public class TextExerciseLevel : ExerciseLevel
    {
        public string Description { get; }

        public TextExerciseLevel(DifficultyRange difficultyRange, string description) : base(difficultyRange)
        {
            Description = description;
        }
    }
}
namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Base class for all types of exercise levels.
    /// </summary>
    public abstract class ExerciseLevel
    {
        public DifficultyRange DifficultyRange { get; }

        protected ExerciseLevel(DifficultyRange difficultyRange)
        {
            DifficultyRange = difficultyRange;
        }
    }
}
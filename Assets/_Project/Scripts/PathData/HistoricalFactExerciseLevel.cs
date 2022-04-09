namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Exercise level that displays a historical fact.
    /// </summary>
    public class HistoricalFactExerciseLevel : TextExerciseLevel
    {
        public string Fact => Description;
        
        public HistoricalFactExerciseLevel(DifficultyRange difficultyRange, string fact) : base(difficultyRange, fact)
        {
        }
    }
}
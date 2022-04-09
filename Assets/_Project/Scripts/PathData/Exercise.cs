using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds data for an exercise.
    /// </summary>
    public class Exercise
    {
        public string DisplayedName { get; }
        public ExerciseCategory Category { get; }
        public ExerciseSubcategory Subcategory { get; }
        public List<ExerciseLevel> Levels { get; }
        public string ExerciseId { get; }

        public Exercise(string exerciseId, string displayedName, ExerciseCategory category, ExerciseSubcategory subcategory, List<ExerciseLevel> levels)
        {
            ExerciseId = exerciseId;
            DisplayedName = displayedName;
            Category = category;
            Subcategory = subcategory;
            Levels = levels;
        }
    }
}
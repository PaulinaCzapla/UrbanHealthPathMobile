using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Quiz that has a text question and multiple image answers.
    /// </summary>
    public class ImageSelectionExerciseLevel : ExerciseLevel
    {
        public string Question { get; }
        public IList<LateBoundValue<MediaFile>> Images { get; }
        public IList<int> CorrectAnswers { get; }

        public ImageSelectionExerciseLevel(DifficultyRange difficultyRange, string question,
            IList<LateBoundValue<MediaFile>> images, IList<int> correctAnswers) : base(difficultyRange)
        {
            Question = question;
            Images = images;
            CorrectAnswers = correctAnswers;
        }
    }
}
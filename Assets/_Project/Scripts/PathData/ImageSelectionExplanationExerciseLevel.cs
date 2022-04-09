using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Explanations of selectable answers for ImageSelectionExercise.
    /// </summary>
    public class ImageSelectionExplanationExerciseLevel : ExerciseLevel
    {
        public IList<LateBoundValue<MediaFile>> Images { get; }
        public IList<string> Explanations { get; }

        public ImageSelectionExplanationExerciseLevel(DifficultyRange difficultyRange,
            IList<LateBoundValue<MediaFile>> images, IList<string> explanations) : base(difficultyRange)
        {
            Images = images;
            Explanations = explanations;
        }
    }
}
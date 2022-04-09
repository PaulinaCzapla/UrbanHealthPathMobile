using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Interface representing loader of exercises.
    /// </summary>
    public interface IExercisesLoader
    {
        IList<Exercise> LoadExercises();
    }
}
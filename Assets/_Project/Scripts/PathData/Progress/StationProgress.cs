using System;
using System.Collections.Generic;
using System.Linq;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Class representing progress of exercises of a station.
    /// </summary>
    public class StationProgress
    {
        public event Action<Station, Exercise> ExerciseCompleted;
        public event Action<Station, ExerciseCategory> CategoryCompleted;

        private readonly Station _station;

        private readonly Dictionary<ExerciseCategory, IReadOnlyList<Exercise>> _categoriesCache;
        private readonly Dictionary<ExerciseCategory, int> _categoriesProgress;

        public StationProgress(Station station)
        {
            _station = station;
            _categoriesCache = new Dictionary<ExerciseCategory, IReadOnlyList<Exercise>>();
            _categoriesProgress = new Dictionary<ExerciseCategory, int>();

            InitializeCategories(Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>());
        }

        public Exercise GetCurrentExercise(ExerciseCategory category)
        {
            return IsCategoryFinished(category) ? null : _categoriesCache[category][_categoriesProgress[category]];
        }

        public Exercise CompleteCurrentExercise(ExerciseCategory category)
        {
            Exercise completedExercise = GetCurrentExercise(category);
            _categoriesProgress[category]++;
            
            ExerciseCompleted?.Invoke(_station, completedExercise);

            if (IsCategoryFinished(category))
            {
                CategoryCompleted?.Invoke(_station, category);
            }

            return completedExercise;
        }

        public bool IsFinished()
        {
            return _categoriesProgress.All(x => IsCategoryFinished(x.Key));
        }

        public bool IsCategoryFinished(ExerciseCategory category)
        {
            return _categoriesProgress[category] >= _categoriesCache[category].Count;
        }

        public bool IsOnLastExerciseForCategory(ExerciseCategory category)
        {
            return _categoriesProgress[category] == _categoriesCache[category].Count - 1;
        }

        private void InitializeCategories(IEnumerable<ExerciseCategory> categories)
        {
            foreach (ExerciseCategory category in categories)
            {
                CacheCategory(category);
                InitializeProgress(category);
            }
        }

        private void CacheCategory(ExerciseCategory category)
        {
            _categoriesCache.Add(category, _station.GetExercisesOfCategory(category));
        }

        private void InitializeProgress(ExerciseCategory category)
        {
            _categoriesProgress.Add(category, 0);
        }
    }
}
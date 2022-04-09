using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.PathData.DataLoaders;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that sets up bindings and contains all path-related data.
    /// </summary>
    public class ApplicationData : IApplicationData
    {
        public IList<MediaFile> MediaFiles { get; }
        public IList<Exercise> Exercises { get; }
        public IList<Waypoint> Waypoints { get; }
        public IList<UrbanPath> UrbanPaths { get; }
        public IList<Test> Tests { get; }

        public ApplicationData(IList<MediaFile> mediaFiles, IList<Exercise> exercises, IList<Waypoint> waypoints,
            IList<UrbanPath> urbanPaths, IList<Test> tests)
        {
            MediaFiles = mediaFiles;
            Exercises = exercises;
            Waypoints = waypoints;
            UrbanPaths = urbanPaths;
            Tests = tests;
        }

        public void SetLateBindings()
        {
            SetStationsLateBindings();
            SetTestsLateBindings();
            SetUrbanPathsLateBindings();
            SetExercisesLateBindings();
        }

        private void SetStationsLateBindings()
        {
            List<Station> stations = Waypoints.OfType<Station>().ToList();

            foreach (Station station in stations)
            {
                foreach (LateBoundValue<Exercise> exercise in station.Exercises)
                {
                    exercise.InitializeValue(Exercises.FirstOrDefault(x => x.ExerciseId == exercise.Key));
                }

                station.NavigationAudio.InitializeValue(MediaFiles.FirstOrDefault(x =>
                    x.MediaId == station.NavigationAudio.Key));
                station.Image.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == station.Image.Key));
                station.IntroductionAudio.InitializeValue(MediaFiles.FirstOrDefault(x =>
                    x.MediaId == station.IntroductionAudio.Key));
            }
        }

        private void SetTestsLateBindings()
        {
            foreach (Test test in Tests)
            {
                foreach (LateBoundValue<Exercise> exercise in test.Exercises)
                {
                    exercise.InitializeValue(Exercises.FirstOrDefault(x => x.ExerciseId == exercise.Key));
                }
            }
        }

        private void SetUrbanPathsLateBindings()
        {
            foreach (UrbanPath urbanPath in UrbanPaths)
            {
                foreach (LateBoundValue<Waypoint> waypoint in urbanPath.Waypoints)
                {
                    waypoint.InitializeValue(Waypoints.FirstOrDefault(x => x.WaypointId == waypoint.Key));
                }

                urbanPath.PreviewImage.InitializeValue(MediaFiles.FirstOrDefault(x =>
                    x.MediaId == urbanPath.PreviewImage.Key));
                
                urbanPath.Icon.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == urbanPath.Icon.Key));
            }
        }

        private void SetExercisesLateBindings()
        {
            foreach (Exercise exercise in Exercises)
            {
                List<VideoExerciseLevel> videoExerciseLevels = exercise.Levels.OfType<VideoExerciseLevel>().ToList();

                foreach (VideoExerciseLevel videoExerciseLevel in videoExerciseLevels)
                {
                    videoExerciseLevel.VideoFile.InitializeValue(
                        MediaFiles.FirstOrDefault(x => x.MediaId == videoExerciseLevel.VideoFile.Key));
                }

                List<ImageExerciseLevel> imageExerciseLevels = exercise.Levels.OfType<ImageExerciseLevel>().ToList();

                foreach (ImageExerciseLevel imageExerciseLevel in imageExerciseLevels)
                {
                    imageExerciseLevel.ImageFile.InitializeValue(
                        MediaFiles.FirstOrDefault(x => x.MediaId == imageExerciseLevel.ImageFile.Key));
                }

                List<ImageSelectionExerciseLevel> imageSelectionExerciseLevels =
                    exercise.Levels.OfType<ImageSelectionExerciseLevel>().ToList();

                foreach (ImageSelectionExerciseLevel imageSelectionExerciseLevel in imageSelectionExerciseLevels)
                {
                    foreach (LateBoundValue<MediaFile> image in imageSelectionExerciseLevel.Images)
                    {
                        image.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == image.Key));
                    }
                }
                
                List<ImageSelectionExplanationExerciseLevel> imageSelectionExplanationExerciseLevels =
                    exercise.Levels.OfType<ImageSelectionExplanationExerciseLevel>().ToList();

                foreach (ImageSelectionExplanationExerciseLevel imageSelectionExplanationExerciseLevel in imageSelectionExplanationExerciseLevels)
                {
                    foreach (LateBoundValue<MediaFile> image in imageSelectionExplanationExerciseLevel.Images)
                    {
                        image.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == image.Key));
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds information about a station - waypoint that user can interact with.
    /// </summary>
    public class Station : Waypoint
    {
        public string DisplayedName { get; }
        public IList<LateBoundValue<Exercise>> Exercises { get; }
        public LateBoundValue<MediaFile> NavigationAudio { get; }
        public LateBoundValue<MediaFile> Image { get; }
        public LateBoundValue<MediaFile> IntroductionAudio { get; }

        public Station(string waypointId, Coordinates coordinates, string zoneName, string displayedName,
            IList<LateBoundValue<Exercise>> exercises,
            LateBoundValue<MediaFile> navigationAudio, LateBoundValue<MediaFile> image, LateBoundValue<MediaFile> introductionAudio) : base(waypointId, coordinates, zoneName)
        {
            DisplayedName = displayedName;
            Exercises = exercises;
            NavigationAudio = navigationAudio;
            Image = image;
            IntroductionAudio = introductionAudio;
        }

        public IReadOnlyList<Exercise> GetExercisesOfCategory(ExerciseCategory category)
        {
            return Exercises.Select(x => x.Value).Where(x => x != null && x.Category == category).ToArray();
        }
    }
}
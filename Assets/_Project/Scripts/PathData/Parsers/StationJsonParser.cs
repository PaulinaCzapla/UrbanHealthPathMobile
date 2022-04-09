using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;
using Random = System.Random;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into Station.
    /// </summary>
    public class StationJsonParser : ValidatedJsonObjectParser<Station>
    {
        private const string ID_KEY = "waypoint_id";
        private const string COORDINATES_KEY = "coordinates";
        private const string ZONE_NAME_KEY = "zone_name";
        private const string EXERCISES_KEY = "exercises";
        private const string DISPLAYED_NAME_KEY = "displayed_name";
        private const string NAVIGATION_AUDIO_KEY = "navigation_audio";
        private const string IMAGE_KEY = "image";
        private const string INTRODUCTION_AUDIO_KEY = "introduction_audio";

        public StationJsonParser() : base(new[]
        {
            ID_KEY, COORDINATES_KEY, ZONE_NAME_KEY, EXERCISES_KEY, DISPLAYED_NAME_KEY,
            NAVIGATION_AUDIO_KEY, IMAGE_KEY, INTRODUCTION_AUDIO_KEY
        })
        {
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            if (json[COORDINATES_KEY].Type != JTokenType.Array)
            {
                throw new ParsingException();
            }
        }

        protected override Station ParseJsonObject(JObject json)
        {
            Coordinates coordinates = ParseCoordinates(json);
            List<LateBoundValue<Exercise>> exercises = ParseExercises(json);
            LateBoundValue<MediaFile> navigationAudio =
                new LateBoundValue<MediaFile>(json[NAVIGATION_AUDIO_KEY].Value<string>());
            LateBoundValue<MediaFile> image = new LateBoundValue<MediaFile>(json[IMAGE_KEY].Value<string>());
            LateBoundValue<MediaFile> introductionAudio =
                new LateBoundValue<MediaFile>(json[INTRODUCTION_AUDIO_KEY].Value<string>());

            return new Station(json[ID_KEY].Value<string>(), coordinates, json[ZONE_NAME_KEY].Value<string>(),
                json[DISPLAYED_NAME_KEY].Value<string>(), exercises, navigationAudio, image,
                introductionAudio);
        }

        private Coordinates ParseCoordinates(JObject json)
        {
            JArray coordinatesArray = (JArray) json[COORDINATES_KEY];
            return new Coordinates(coordinatesArray[0].Value<double>(), coordinatesArray[1].Value<double>());
        }

        private List<LateBoundValue<Exercise>> ParseExercises(JObject json)
        {
            List<LateBoundValue<Exercise>> exercises = new List<LateBoundValue<Exercise>>();

            JArray jsonExercises = (JArray) json[EXERCISES_KEY];

            if (jsonExercises.HasValues)
            {
                JToken exerciseGroup = GetExerciseGroupToAdd(jsonExercises);

                foreach (JToken exercise in exerciseGroup)
                {
                    exercises.Add(new LateBoundValue<Exercise>((string) exercise));
                }
            }

            return exercises;
        }

        private JToken GetExerciseGroupToAdd(JArray jsonExercises)
        {
            bool hasExerciseGroups = jsonExercises.Any(x => x.Type == JTokenType.Array);

            return hasExerciseGroups ? jsonExercises[new Random().Next(0, jsonExercises.Count)] : jsonExercises;
        }
    }
}
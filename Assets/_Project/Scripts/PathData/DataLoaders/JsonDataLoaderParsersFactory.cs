using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Factory that returns DataLoaderParsers for JSON data.
    /// </summary>
    public class JsonDataLoaderParsersFactory
    {
        public DataLoaderParsers<JObject> Create()
        {
            JsonObjectParser<MediaFile> mediaFilesParser = new MediaFileJsonParser();
            JsonObjectParser<Exercise> exercisesParser = CreateExercisesParser();
            JsonObjectParser<Waypoint> waypointsParser = CreateWaypointsParser();
            JsonObjectParser<UrbanPath> urbanPathsParser = new UrbanPathJsonParser();
            JsonObjectParser<Test> testParser = new TestJsonParser();

            return new DataLoaderParsers<JObject>(mediaFilesParser, exercisesParser,
                waypointsParser, urbanPathsParser, testParser);
        }

        private JsonObjectParser<Exercise> CreateExercisesParser()
        {
            JsonObjectParser<ExerciseLevel>
                exerciseLevelsParser = new ExerciseLevelJsonParser(new ExerciseLevelTypesJsonParsersFactory());

            return new ExerciseJsonParser(exerciseLevelsParser);
        }

        private JsonObjectParser<Waypoint> CreateWaypointsParser()
        {
            JsonObjectParser<Station> stationsParser = new StationJsonParser();

            return new WaypointJsonParser(stationsParser);
        }
    }
}
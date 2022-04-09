using System.IO;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Factory of JSON loaders that gets the data to be loaded from .json files.
    /// </summary>
    public class JsonFilesLoadersFactory : ILoadersFactory
    {
        private readonly string _filesDirectoryPath;
        private readonly IJsonFileReader _fileReader;
        private readonly DataLoaderParsers<JObject> _parsers;
        
        public JsonFilesLoadersFactory(string filesDirectoryPath, IJsonFileReader fileReader, DataLoaderParsers<JObject> parsers)
        {
            _filesDirectoryPath = filesDirectoryPath;
            _fileReader = fileReader;
            _parsers = parsers;
        }
        
        public IMediaFilesLoader CreateMediaFilesLoader()
        {
            return new JsonMediaFilesLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("media_files")), _parsers.MediaFileParser);
        }

        public IExercisesLoader CreateExercisesLoader()
        {
            return new JsonExercisesLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("exercises")),
                _parsers.ExerciseParser);
        }

        public IWaypointsLoader CreateWaypointsLoader()
        {
            return new JsonWaypointsLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("waypoints")),
                _parsers.WaypointParser);
        }

        public IUrbanPathsLoader CreateUrbanPathsLoader()
        {
            return new JsonUrbanPathsLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("urban_paths")),
                _parsers.UrbanPathParser);
        }

        public ITestLoader CreateTestLoader()
        {
            return new JsonTestLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("tests")),
                _parsers.TestParser);
        }
        
        private string GetFullFilePath(string fileName)
        {
            return Path.Combine(_filesDirectoryPath, fileName);
        }
    }
}
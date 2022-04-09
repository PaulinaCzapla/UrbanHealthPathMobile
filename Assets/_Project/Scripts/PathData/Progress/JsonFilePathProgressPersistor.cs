using System;
using System.IO;
using Newtonsoft.Json;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Class responsible for storing and loading path progress in/from JSON files.
    /// </summary>
    public class JsonFilePathProgressPersistor : IPathProgressPersistor
    {
        private readonly string _filePath;
        private readonly JsonSerializer _serializer;

        public JsonFilePathProgressPersistor(string filePath, JsonSerializer serializer)
        {
            _filePath = filePath;
            _serializer = serializer;
        }
        
        public PathProgress LoadPathProgress()
        {
            try
            {
                using StreamReader file = File.OpenText(_filePath);
                return (PathProgress) _serializer.Deserialize(file, typeof(PathProgress));
            }
            catch (IOException ex)
            {
                
            }
            catch (UnauthorizedAccessException ex)
            {
                
            }
            
            return null;
        }

        public bool SavePathProgress(PathProgress progress)
        {
            try
            {
                using StreamWriter file = File.CreateText(_filePath);
                _serializer.Serialize(file, progress);
            }
            catch (IOException ex)
            {

            }
            catch (UnauthorizedAccessException ex)
            {
                
            }
            
            return false;
        }
    }
}
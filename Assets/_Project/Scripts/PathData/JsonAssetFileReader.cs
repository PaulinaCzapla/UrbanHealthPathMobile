using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that reads a JSON file as a text asset and returns its content.
    /// </summary>
    public class JsonAssetFileReader : IJsonFileReader
    {
        public JToken ReadJsonFromFile(string filePath)
        {
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if (file == null)
            {
                throw new FileNotFoundException($"Specified file {filePath} was not found!");
            }

            return JsonConvert.DeserializeObject<JToken>(file.text);
        }
    }
}
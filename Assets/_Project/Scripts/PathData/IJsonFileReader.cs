using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Interface defining functionality of reading JSON file.
    /// </summary>
    public interface IJsonFileReader
    {
        JToken ReadJsonFromFile(string filePath);
    }
}
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Base class of parsers of JObjects that validate the given object before parsing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValidatedJsonObjectParser<T> : JsonObjectParser<T>
    {
        private readonly string[] _requiredKeys;

        protected ValidatedJsonObjectParser(string[] requiredKeys)
        {
            _requiredKeys = requiredKeys;
        }

        public override T Parse(JObject parsedValue)
        {
            ValidateJson(parsedValue);

            return base.Parse(parsedValue);
        }

        protected virtual void ValidateJson(JObject json)
        {
            if (!CheckIfAllRequiredKeysExists(json))
            {
                throw new ParsingException($"Not all required keys found! {json}");
            }
        }
        
        private bool CheckIfAllRequiredKeysExists(JObject json)
        {
            return _requiredKeys.All(json.ContainsKey);
        }
    }
}
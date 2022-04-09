using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class ObjectPrefsValueReader<T> : IPrefsValueReader<T>
    {
        private readonly IParser<string, T> _parser;
        
        public ObjectPrefsValueReader(IParser<string, T> parser)
        {
            _parser = parser;
        }
        
        public T Read(string prefsKey)
        {
            return _parser.Parse(PlayerPrefs.GetString(prefsKey));
        }
    }
}
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class ObjectPrefsValueWriter<T> : IPrefsValueWriter<T>
    {
        private readonly IParser<T, string> _parser;

        public ObjectPrefsValueWriter(IParser<T, string> parser)
        {
            _parser = parser;
        }

        public void Write(string prefsKey, T value)
        {
            PlayerPrefs.SetString(prefsKey, _parser.Parse(value));
        }
    }
}
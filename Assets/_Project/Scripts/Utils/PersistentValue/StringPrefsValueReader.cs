using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Reads PlayerPrefs string values.
    /// </summary>
    public class StringPrefsValueReader : IPrefsValueReader<string>
    {
        public string Read(string prefsKey)
        {
            return PlayerPrefs.GetString(prefsKey);
        }
    }
}
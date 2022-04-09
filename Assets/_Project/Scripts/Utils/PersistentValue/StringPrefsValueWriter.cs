using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Writes PlayerPrefs string values.
    /// </summary>
    public class StringPrefsValueWriter : IPrefsValueWriter<string>
    {
        public void Write(string prefsKey, string value)
        {
            PlayerPrefs.SetString(prefsKey, value);
        }
    }
}
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class BoolPrefsValueReader : IPrefsValueReader<bool>
    {
        public bool Read(string prefsKey)
        {
            return PlayerPrefs.GetInt(prefsKey) == 1;
        }
    }
}
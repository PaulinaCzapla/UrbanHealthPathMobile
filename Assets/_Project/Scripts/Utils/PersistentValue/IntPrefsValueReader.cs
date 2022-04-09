using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class IntPrefsValueReader : IPrefsValueReader<int>
    {
        public int Read(string prefsKey)
        {
            return PlayerPrefs.GetInt(prefsKey);
        }
    }
}
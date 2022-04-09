using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class FloatPrefsValueReader : IPrefsValueReader<float>
    {
        public float Read(string prefsKey)
        {
            return PlayerPrefs.GetFloat(prefsKey);
        }
    }
}
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class FloatPrefsValueWriter : IPrefsValueWriter<float>
    {
        public void Write(string prefsKey, float value)
        {
            PlayerPrefs.SetFloat(prefsKey, value);
        }
    }
}
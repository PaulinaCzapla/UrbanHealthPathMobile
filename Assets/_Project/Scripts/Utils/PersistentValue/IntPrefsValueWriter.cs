using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class IntPrefsValueWriter : IPrefsValueWriter<int>
    {
        public void Write(string prefsKey, int value)
        {
            PlayerPrefs.SetInt(prefsKey, value);
        }
    }
}
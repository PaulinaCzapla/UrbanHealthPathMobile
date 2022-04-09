using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class BoolPrefsValueWriter : IPrefsValueWriter<bool>
    {
        public void Write(string prefsKey, bool value)
        {
            PlayerPrefs.SetInt(prefsKey, value ? 1 : 0);
        }
    }
}
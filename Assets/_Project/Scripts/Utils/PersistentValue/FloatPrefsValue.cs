namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class FloatPrefsValue : PrefsValue<float>
    {
        public FloatPrefsValue(string prefsKey, float defaultValue) : base(prefsKey, defaultValue, new FloatPrefsValueReader(), new FloatPrefsValueWriter())
        {
        }

        public FloatPrefsValue(string prefsKey) : base(prefsKey, new FloatPrefsValueReader(), new FloatPrefsValueWriter())
        {
        }
    }
}
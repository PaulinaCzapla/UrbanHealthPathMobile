namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class BoolPrefsValue : PrefsValue<bool>
    {
        public BoolPrefsValue(string prefsKey, bool defaultValue) : base(prefsKey, defaultValue, new BoolPrefsValueReader(), new BoolPrefsValueWriter())
        {
        }

        public BoolPrefsValue(string prefsKey) : base(prefsKey, new BoolPrefsValueReader(), new BoolPrefsValueWriter())
        {
        }
    }
}
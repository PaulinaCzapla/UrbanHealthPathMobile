namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class IntPrefsValue : PrefsValue<int>
    {
        public IntPrefsValue(string prefsKey, int defaultValue) : base(prefsKey, defaultValue,
            new IntPrefsValueReader(), new IntPrefsValueWriter())
        {
            
        }
        public IntPrefsValue(string prefsKey) : base(prefsKey, new IntPrefsValueReader(), new IntPrefsValueWriter())
        {
        }
    }
}
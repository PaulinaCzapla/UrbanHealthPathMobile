namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    public class ObjectPrefsValue<T> : PrefsValue<T>
    {
        public ObjectPrefsValue(string prefsKey, T defaultValue, IParser<string, T> readParser, IParser<T, string> writeParser) 
            : base(prefsKey, defaultValue, new ObjectPrefsValueReader<T>(readParser), new ObjectPrefsValueWriter<T>(writeParser))
        {
        }

        public ObjectPrefsValue(string prefsKey, IParser<string, T> readParser, IParser<T, string> writeParser) 
            : base(prefsKey, new ObjectPrefsValueReader<T>(readParser), new ObjectPrefsValueWriter<T>(writeParser))
        {
        }
    }
}
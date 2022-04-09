namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Interface that defines method for writing a prefs value.
    /// </summary>
    /// <typeparam name="T">Written value type.</typeparam>
    public interface IPrefsValueWriter<in T>
    {
        void Write(string prefsKey, T value);
    }
}
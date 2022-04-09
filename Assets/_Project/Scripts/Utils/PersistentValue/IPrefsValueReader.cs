namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Interface that defines method for reading a prefs value.
    /// </summary>
    /// <typeparam name="T">Read value type.</typeparam>
    public interface IPrefsValueReader<out T>
    {
        T Read(string prefsKey);
    }
}
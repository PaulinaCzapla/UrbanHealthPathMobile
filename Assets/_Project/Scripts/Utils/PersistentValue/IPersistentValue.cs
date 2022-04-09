namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Interface defining a value that can be persisted between sessions.
    /// </summary>
    /// <typeparam name="T">Type of persisted value.</typeparam>
    public interface IPersistentValue<T>
    {
        T Value { get; set; }
    }
}
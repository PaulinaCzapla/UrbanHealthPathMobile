namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class representing late-bound value - data that is loaded later based on key from another data source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LateBoundValue<T>
    {
        private bool _isInitialized;
        public string Key { get; }
        public T Value { get; private set; }

        public LateBoundValue(string key)
        {
            Key = key;
        }

        public void InitializeValue(T valueToInitializeWith)
        {
            if (!_isInitialized)
            {
                Value = valueToInitializeWith;
                _isInitialized = true;
            }
        }

        public static implicit operator T(LateBoundValue<T> lateBoundValue) => lateBoundValue.Value;
    }
}
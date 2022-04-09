using System;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PersistentValue
{
    /// <summary>
    /// Implementation of IPersistentValue that uses Unity's PlayerPrefs system.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrefsValue<T> : IPersistentValue<T>
    {
        private readonly string _prefsKey;
        private readonly IPrefsValueReader<T> _reader;
        private readonly IPrefsValueWriter<T> _writer;

        private readonly T _defaultValue;
        private readonly bool _hasDefaultValue;
        
        public T Value
        {
            get => GetValue();
            set => SaveValue(value);
        }
        
        public PrefsValue(string prefsKey, T defaultValue, IPrefsValueReader<T> reader, IPrefsValueWriter<T> writer) : this(prefsKey, reader, writer)
        {
            _defaultValue = defaultValue;
            _hasDefaultValue = true;
        }
        
        public PrefsValue(string prefsKey, IPrefsValueReader<T> reader, IPrefsValueWriter<T> writer)
        {
            _prefsKey = prefsKey;
            _reader = reader;
            _writer = writer;
        }
        
        private T GetValue()
        {
            if (PlayerPrefs.HasKey(_prefsKey))
            {
                return _reader.Read(_prefsKey);
            }

            if (_hasDefaultValue)
            {
                return _defaultValue;
            }

            throw new ArgumentException("No value in PlayerPrefs with given key.", nameof(_prefsKey));
        }

        private void SaveValue(T value)
        {
            _writer.Write(_prefsKey, value);
            PlayerPrefs.Save();
        }
    }
}
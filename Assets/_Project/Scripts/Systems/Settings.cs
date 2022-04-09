using System;
using PolSl.UrbanHealthPath.Utils.PersistentValue;

namespace PolSl.UrbanHealthPath.Systems
{
    /// <summary>
    /// Settings system that allow manipulating application's settings.
    /// </summary>
    public class Settings
    {
        public event Action<bool> IsAudioEnabledChanged;
        
        private readonly IPersistentValue<bool> _isAudioEnabled = new BoolPrefsValue("Settings_IsAudioEnabled", true);

        public bool IsAudioEnabled
        {
            get => _isAudioEnabled.Value;
            set => SetIsAudioEnabled(value);
        }

        private void SetIsAudioEnabled(bool value)
        {
            _isAudioEnabled.Value = value;
            IsAudioEnabledChanged?.Invoke(value);
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    /// <summary>
    /// Represents a UI list element.  
    /// </summary>
    public class ListPanelElement : MonoBehaviour
    {
        [FormerlySerializedAs("buttonText")] [SerializeField] private TextMeshProUGUI _buttonText;
        [FormerlySerializedAs("iconText")] [SerializeField] private TextMeshProUGUI _iconText;
        [FormerlySerializedAs("button")] [SerializeField] private Button _button;
        [FormerlySerializedAs("icon")] [SerializeField] private RawImage _icon;

        public void SetValues(string buttonText, string iconText, UnityAction action, Texture2D icon, Color iconColor)
        {
            SetValues(buttonText, iconText, action, icon);
            _icon.color = iconColor;
        }

        public void SetValues(string buttonText, string iconText, UnityAction action, Texture2D icon = null)
        {
            this._buttonText.text = buttonText ?? "";
            this._iconText.text = iconText ?? "";
            this._button.onClick.AddListener(() => action?.Invoke());

            if (icon != null)
            {
                this._icon.texture = icon;
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
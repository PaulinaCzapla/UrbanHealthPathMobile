using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Changes the appearance of the button (image and text) when clicked.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public partial class AppearanceChangingButton : MonoBehaviour
    {
        [FormerlySerializedAs("buttonAppearances")]
        [Tooltip("List with button appearances, that changes every time user click on the button. " +
                 "First list element is a default button appearance. ")]
        [SerializeField] private List<ButtonAppearance> _buttonAppearances;
        [FormerlySerializedAs("image")] [SerializeField] private Image _image;
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("button")] [SerializeField] private Button _button;

        private int _lastAppearanceIndex = 1;

        private void Awake()
        {
            _button.onClick.AddListener(ChangeImageAndText);
        }
        
        public void SetDefaultAppearance()
        {
            if (_buttonAppearances != null)
            {
                _image.sprite = _buttonAppearances[0].GetSprite();
                _text.text = _buttonAppearances[0].GetText();
                _text.margin = new Vector4(0, 0, 0, _buttonAppearances[0].GetOffset());
                _lastAppearanceIndex++;
            }
        }

        private void ChangeImageAndText()
        {
            if (_lastAppearanceIndex > _buttonAppearances.Count - 1)
            {
                _lastAppearanceIndex = 0;
            }

            _image.sprite = _buttonAppearances[_lastAppearanceIndex].GetSprite();
            _text.text = _buttonAppearances[_lastAppearanceIndex].GetText();
            _text.margin = new Vector4(0, 0, 0, _buttonAppearances[_lastAppearanceIndex].GetOffset());
            _lastAppearanceIndex++;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ChangeImageAndText);
        }
    }
}
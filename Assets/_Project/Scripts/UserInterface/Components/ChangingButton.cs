using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// A button that can change it's appearance.  
    /// </summary>
    /// 
    [RequireComponent(typeof(Button))]
    public class ChangingButton : MonoBehaviour
    {
        public Button Button { get; private set; }
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private string _defaultText;
        [SerializeField] private Vector4 _defaultMargin;

        private void Awake()
        {
            Button = GetComponent<Button>();
            SetDefaultAppearance();
        }


        public void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetButtonText(string text, Vector4 margins)
        {
            _text.text = text;
            _text.margin = margins;
        }

        public void SetDefaultAppearance()
        {
            _icon.sprite = _defaultSprite;
            _text.text = _defaultText;
            _text.margin = _defaultMargin;
        }

        public void SetInteractable(bool isInteractable)
        {
            Button.interactable = isInteractable;
        }
    }
}
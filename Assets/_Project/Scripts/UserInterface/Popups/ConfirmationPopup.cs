using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents confirmation popup. This object can be initialized with ConfirmationPopupInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class ConfirmationPopup :MonoBehaviour, IPopup, IInitializablePopup, IDisplayable
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("buttonFirst")] [SerializeField] private Button _buttonFirst;
        [FormerlySerializedAs("buttonSecond")] [SerializeField] private Button _buttonSecond;
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is ConfirmationPopupInitializationParameters init)
            {
                _text.text = init.Information;

                if (init.NumberOfButtons == 0)
                {
                    _buttonFirst.gameObject.SetActive(false);
                    _buttonSecond.gameObject.SetActive(false);
                }
                else if (init.NumberOfButtons == 1)
                {
                    _buttonFirst.gameObject.SetActive(true);
                    _buttonSecond.gameObject.SetActive(false);

                    if (init.ButtonTexts.Length >= 1)
                    {
                        _buttonFirst.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[0];
                    }

                    if (init.Actions.Length >= 1)
                    {
                        _buttonFirst.onClick.AddListener(init.Actions[0]);
                    }
                }
                else
                {
                    _buttonFirst.gameObject.SetActive(true);
                    _buttonSecond.gameObject.SetActive(true);

                    if (init.ButtonTexts.Length >= 2)
                    {
                        _buttonFirst.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[0];
                        _buttonSecond.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[1];
                    }

                    if (init.Actions.Length >= 2)
                    {
                        _buttonFirst.onClick.AddListener(() => init.Actions[0]?.Invoke());
                        _buttonSecond.onClick.AddListener(() => init.Actions[1]?.Invoke());
                    }
                }
            }
        }

        public void Display()
        {
            throw new NotImplementedException();
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }
        private void OnDisable()
        {
            _buttonFirst.onClick.RemoveAllListeners();
            _buttonSecond.onClick.RemoveAllListeners();
        }
    }
}
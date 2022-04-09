using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a quiz with images popup. This object can be initialized with QuizWithImagesPopupInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class QuizWithImagesPopup : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [SerializeField] private ButtonFitterConnection[] _buttonFitterConnections;
        
        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is QuizWithImagesPopupInitializationParameters init)
            {
                InitSizeAndPosition(init.Payload);
                _text.text = init.Question;
                
                for (int i = 0; i < _buttonFitterConnections.Length; i++)
                {
                    int index = i;
                    _buttonFitterConnections[index].ImageFitter.InitializeImage(init.QuizElementOptions[index].Texture);
                    _buttonFitterConnections[index].Button.onClick.AddListener(()=>
                        init.QuizElementOptions[index].ButtonTextureAction?.Invoke(_buttonFitterConnections[index].Button.GetComponent<QuizWithImageOptionButton>()));
                }
            }
        }
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnDisable()
        {
            foreach (var buttonFitterConnection in _buttonFitterConnections)
            {
                buttonFitterConnection.Button.onClick.RemoveAllListeners();
            }
        }
    }
}
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class QuizExplanationPopup : MonoBehaviour, IPopup, IInitializablePopup
    {
        /// <summary>
        /// A class that represents an explanation to a quiz with images.
        /// This object can be initialized with QuizExplanationPopupInitializationParameters.
        /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
        /// </summary>
        public RectTransform PopupArea => _popupArea;

        [SerializeField] private RectTransform _popupArea;
        [SerializeField] private TextMeshProUGUI _textOne;
        [SerializeField] private TextMeshProUGUI _textTwo;
        [SerializeField] private TextMeshProUGUI _textThree;
        [SerializeField] private TextMeshProUGUI _textFour;
        [SerializeField] private ImageFitter _imageOne;
        [SerializeField] private ImageFitter _imageTwo;
        [SerializeField] private ImageFitter _imageThree;
        [SerializeField] private ImageFitter _imageFour;

        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is QuizExplanationPopupInitializationParameters init)
            {
                InitSizeAndPosition(init.Payload);
                _textOne.text = init.Explanations[0];
                _textTwo.text = init.Explanations[1];
                _textThree.text = init.Explanations[2];
                _textFour.text = init.Explanations[3];
                
                _imageOne.InitializeImage(init.Images[0]);
                _imageTwo.InitializeImage(init.Images[1]);
                _imageThree.InitializeImage(init.Images[2]);
                _imageFour.InitializeImage(init.Images[3]);
            }
        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }
    }
}

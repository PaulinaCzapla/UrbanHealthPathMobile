using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that represents a quiz with images texts. This object can be initialized with QuizWithTextPopupInitializationParameters.
    /// It is extended by IPopup interface, so it's size and position should be initialized with PopupPayload.
    /// </summary>
    public class QuizWithTextsPopup : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("question")] [SerializeField] private TextMeshProUGUI _question;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("text1")] [SerializeField] private TextMeshProUGUI _text1;
        [FormerlySerializedAs("text2")] [SerializeField] private TextMeshProUGUI _text2;
        [FormerlySerializedAs("text3")] [SerializeField] private TextMeshProUGUI _text3;
        [FormerlySerializedAs("text4")] [SerializeField] private TextMeshProUGUI _text4;
        [FormerlySerializedAs("button1")] [SerializeField] private Button _button1;
        [FormerlySerializedAs("button2")] [SerializeField] private Button _button2;
        [FormerlySerializedAs("button3")] [SerializeField] private Button _button3;
        [FormerlySerializedAs("button4")] [SerializeField] private Button _button4;

        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is QuizWithTextPopupInitializationParameters init)
            {
                InitSizeAndPosition(init.Payload);
                
                _question.text = init.Question;
                _text1.text = init.Options[0].Text;
                _text2.text = init.Options[1].Text;
                _text3.text = init.Options[2].Text;
                _text4.text = init.Options[3].Text;

                _button1.onClick.AddListener(() => init.Options[0].ButtonAction?.Invoke(_button1.GetComponent<QuizOptionButton>()));
                _button2.onClick.AddListener(() => init.Options[1].ButtonAction?.Invoke(_button2.GetComponent<QuizOptionButton>()));
                _button3.onClick.AddListener(() => init.Options[2].ButtonAction?.Invoke(_button3.GetComponent<QuizOptionButton>()));
                _button4.onClick.AddListener(() => init.Options[3].ButtonAction?.Invoke(_button4.GetComponent<QuizOptionButton>()));
            }
        }
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnDisable()
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
            _button4.onClick.RemoveAllListeners();
        }
    }
}
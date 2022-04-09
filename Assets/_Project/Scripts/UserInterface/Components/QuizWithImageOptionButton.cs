using TMPro;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Represents a button, that is a quiz with images option. It can display given textual information.
    /// </summary>
    public class QuizWithImageOptionButton:QuizOptionButton
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void EnableRedText(string textToSet)
        {
            _text.enabled = true;
            _text.text = textToSet;
            _text.color = Color.red;
        }
        
        public void EnableGreenText(string textToSet)
        {
            _text.enabled = true;
            _text.text = textToSet;
            _text.color = Color.green;
        }

        public void DisableText()
        {
            _text.enabled = false;
        }
        
        public new void SetDefault()
        {
            DisableFrame();
            SetInteractable(true);
            DisableText();
        }
    }
}
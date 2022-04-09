using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Represents a button, that is a quiz with text option. 
    /// </summary>
    public class QuizWithTextElementOption
    {
        public string Text { get; }
        
        public UnityAction<QuizOptionButton> ButtonAction { get; }

        public QuizWithTextElementOption(string text, UnityAction<QuizOptionButton> buttonAction)
        {
            Text = text;
            ButtonAction = buttonAction;
        }
    }
}
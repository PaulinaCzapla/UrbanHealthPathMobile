using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Represents a button, that is an option in quiz.
    /// </summary>
    public class QuizOptionButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected  RawImage _frame;
        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }

        public void EnableGreenFrame()
        {
            _frame.enabled = true;
            _frame.color = Color.green;
        }

        public void EnableRedFrame()
        {
            _frame.enabled = true;
            _frame.color = Color.red;
        }

        public void DisableFrame()
        {
            _frame.enabled = false;
        }

        public void SetDefault()
        {
            DisableFrame();
            SetInteractable(true);
        }
    }
}

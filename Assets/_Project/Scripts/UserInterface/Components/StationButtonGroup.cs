using System;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Group of Buttons used in the Station View.
    /// </summary>
    public class StationButtonGroup : MonoBehaviour
    {
        [SerializeField] private ChangingButton _sensorialButton;
        [SerializeField] private ChangingButton _motoricalButton;
        [SerializeField] private ChangingButton _gameButton;
        [SerializeField] private Sprite _completeCategoryIcon;
        [SerializeField] private Sprite _nextExerciseIcon;
        
        private Dictionary<ChangingButton, StationButtonState> _buttonStates;

        public void Initialize(UnityAction<StationButtonGroup> initialized)
        {
            _buttonStates = new Dictionary<ChangingButton, StationButtonState>();
            _buttonStates[_sensorialButton] = StationButtonState.Inactive;
            _buttonStates[_motoricalButton] = StationButtonState.Inactive;
            _buttonStates[_gameButton] = StationButtonState.Inactive;
            
            initialized?.Invoke(this);
        }

        private void OnDisable()
        {
            RemoveAllListeners();
        }

        public void AddListenerToCategoryButton(ExerciseCategory category, UnityAction action)
        {
            ChangingButton button = GetButtonForCategory(category);
            
            button.Button.onClick.AddListener(action);
        }

        public StationButtonState GetCategoryButtonState(ExerciseCategory category)
        {
            return _buttonStates[GetButtonForCategory(category)];
        }

        public void SetAllOtherNotFinishedCategoriesToInactive(ExerciseCategory category)
        {
            ChangingButton categoryButton = GetButtonForCategory(category);
            
            foreach (ChangingButton stationButton in _buttonStates.Keys.ToList())
            {
                if (stationButton != categoryButton && _buttonStates[stationButton] != StationButtonState.Finished)
                {
                    UpdateButtonState(stationButton, StationButtonState.Inactive);
                }
            }
        }

        public void SetAllNotFinishedCategoriesToInactive()
        {
            foreach (ChangingButton stationButton in _buttonStates.Keys.ToList())
            {
                if (_buttonStates[stationButton] != StationButtonState.Finished)
                {
                    UpdateButtonState(stationButton, StationButtonState.Inactive);
                }
            }
        }

        public void UpdateCategoryButtonState(ExerciseCategory category, StationButtonState newState)
        {
            ChangingButton button = GetButtonForCategory(category);
            UpdateButtonState(button, newState);
        }

        private void UpdateButtonState(ChangingButton button, StationButtonState newState)
        {
            _buttonStates[button] = newState;

            switch (newState)
            {
                case StationButtonState.Inactive:
                    MarkAsInactive(button);
                    break;
                case StationButtonState.ActiveInProgress:
                    MarkAsInProgress(button);
                    break;
                case StationButtonState.ActiveLast:
                    MarkAsLast(button);
                    break;
                case StationButtonState.Finished:
                    MarkAsFinished(button);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private ChangingButton GetButtonForCategory(ExerciseCategory category)
        {
            return category switch
            {
                ExerciseCategory.Game => _gameButton,
                ExerciseCategory.Motorical => _motoricalButton,
                ExerciseCategory.Sensorial => _sensorialButton,
                _ => throw new ArgumentException("Invalid exercise category!", nameof(category))
            };
        }

        private void MarkAsInactive(ChangingButton button)
        {
            button.SetDefaultAppearance();
            button.SetInteractable(true);
        }

        private void MarkAsInProgress(ChangingButton button)
        {
            button.SetButtonText("Dalej", new Vector4(10, 10, 10, 10));
            button.SetSprite(_nextExerciseIcon);
            button.SetInteractable(true);
        }

        private void MarkAsLast(ChangingButton button)
        {
            button.SetButtonText("Zatwierdź", new Vector4(10, 10, 10, 10));
            button.SetSprite(_completeCategoryIcon);
            button.SetInteractable(true);
        }

        private void MarkAsFinished(ChangingButton button)
        {
            button.SetDefaultAppearance();
            button.SetInteractable(false);
        }

        private void RemoveAllListeners()
        {
            _sensorialButton.Button.onClick.RemoveAllListeners();
            _motoricalButton.Button.onClick.RemoveAllListeners();
            _gameButton.Button.onClick.RemoveAllListeners();
        }
    }
}

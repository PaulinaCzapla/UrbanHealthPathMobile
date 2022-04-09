using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for path's stations.
    /// </summary>
    public class StationController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;

        private StationProgress _currentStationProgress;
        private StationButtonGroup _currentStationButtons;

        private ButtonWithAudio _lastAudioButton;

        public StationController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            Settings settings) : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
            _settings = settings;

            _settings.IsAudioEnabledChanged += SetLastAudioMute;
        }

        public void ShowNextStationConfirmation(Station nextStation, Action confirmed)
        {
            _coroutineManager.StartCoroutine(ShowNextStationConfirmationPopup(nextStation, confirmed));
        }

        public void ShowStation(Station station, Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding,
            Action<Station> stationFinished)
        {
            SetCurrentStation(station);

            ViewManager.OpenView(ViewType.Station);

            bool isAnyGameExercise = _currentStationProgress.GetCurrentExercise(ExerciseCategory.Game) != null;
            bool isAnyMotoricalExercise =
                _currentStationProgress.GetCurrentExercise(ExerciseCategory.Motorical) != null;
            bool isAnySensorialExercise =
                _currentStationProgress.GetCurrentExercise(ExerciseCategory.Sensorial) != null;

            AudioClip introductionAudio = null;

            if (station.IntroductionAudio.Value != null)
            {
                introductionAudio = new AudioFileAccessor(station.IntroductionAudio)?.GetMedia();
            }

            Texture2D stationImage = new TextureFileAccessor(station.Image).GetMedia();

            StationViewInitializationParameters initParams =
                new StationViewInitializationParameters(
                    buttons => ConfigureButtonGroup(isAnyGameExercise, isAnyMotoricalExercise, isAnySensorialExercise,
                        exerciseStarting, exerciseEnding, buttons),
                    () => ShowConfirmation("Czy na pewno chcesz zakończyć ćwiczenia na tym punkcie?",
                        () => stationFinished.Invoke(station)), () =>
                    {
                        PopupManager.CloseCurrentPopup();
                        ReturnToPreviousView();
                    },
                    station.DisplayedName, stationImage, AudioButtonInitializedHandler, AudioButtonHandler,
                    introductionAudio);

            ViewManager.InitializeCurrentView(initParams);
        }

        private void ConfigureButtonGroup(bool isAnyGame, bool isAnyMotorical, bool isAnySensorial,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, StationButtonGroup buttons)
        {
            if (!isAnyGame)
            {
                buttons.UpdateCategoryButtonState(ExerciseCategory.Game, StationButtonState.Finished);
            }
            else
            {
                buttons.AddListenerToCategoryButton(ExerciseCategory.Game,
                    () => ExerciseButtonClicked(ExerciseCategory.Game, exerciseStarting, exerciseEnding));
            }

            if (!isAnyMotorical)
            {
                buttons.UpdateCategoryButtonState(ExerciseCategory.Motorical, StationButtonState.Finished);
            }
            else
            {
                buttons.AddListenerToCategoryButton(ExerciseCategory.Motorical,
                    () => ExerciseButtonClicked(ExerciseCategory.Motorical, exerciseStarting, exerciseEnding));
            }

            if (!isAnySensorial)
            {
                buttons.UpdateCategoryButtonState(ExerciseCategory.Sensorial, StationButtonState.Finished);
            }
            else
            {
                buttons.AddListenerToCategoryButton(ExerciseCategory.Sensorial,
                    () => ExerciseButtonClicked(ExerciseCategory.Sensorial, exerciseStarting, exerciseEnding));
            }
            
            _currentStationButtons = buttons;
        }

        private void ExerciseButtonClicked(ExerciseCategory category,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding)
        {
            StationButtonState state = _currentStationButtons.GetCategoryButtonState(category);
            _currentStationButtons.SetAllOtherNotFinishedCategoriesToInactive(category);
            
            if (state == StationButtonState.Finished)
            {
                return;
            }

            if (state != StationButtonState.Inactive)
            {
                Exercise completedExercise = _currentStationProgress.CompleteCurrentExercise(category);
                exerciseEnding.Invoke(completedExercise);
            }

            StationButtonState nextState;
            
            if (state != StationButtonState.ActiveLast)
            {
                bool isLastExercise = _currentStationProgress.IsOnLastExerciseForCategory(category);
                nextState = isLastExercise
                    ? StationButtonState.ActiveLast
                    : StationButtonState.ActiveInProgress;
            }
            else
            {
                nextState = StationButtonState.Finished;
            }

            _currentStationButtons.UpdateCategoryButtonState(category, nextState);

            if (nextState != StationButtonState.Finished)
            {
                Exercise nextExercise = _currentStationProgress.GetCurrentExercise(category);
                exerciseStarting?.Invoke(nextExercise);
            }
        }

        private IEnumerator ShowNextStationConfirmationPopup(Station nextStation, Action confirmed)
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new TextureFileAccessor(nextStation.Image).GetMedia();
            RectTransform transform = ViewManager.CurrentView.GetComponent<PathView>().PopupArea;

            PopupManager.OpenPopup(PopupType.ConfirmArrival,
                new PopupConfirmArrivalInitializationParameters(() =>
                    {
                        PopupManager.CloseCurrentPopup();
                        confirmed.Invoke();
                    },
                    "Czy jesteś tutaj?", texture,
                    new PopupPayload(transform)));
        }

        private void SetCurrentStation(Station station)
        {
            _currentStationProgress = new StationProgress(station);
        }

        private void AudioButtonInitializedHandler(ButtonWithAudio btn)
        {
            _lastAudioButton = btn;
            SetLastAudioMute(_settings.IsAudioEnabled);
        }

        private void AudioButtonHandler(ButtonWithAudio btn)
        {
            _lastAudioButton = btn;
            btn.ToggleState();
        }

        protected override void ViewOpenedHandler(ViewType type)
        {
            base.ViewOpenedHandler(type);

            StopLastAudioIntroduction();
            ResetStationButtons();
        }

        protected override void PopupOpenedHandler(PopupType type)
        {
            base.PopupOpenedHandler(type);

            StopLastAudioIntroduction();

            if (type == PopupType.Confirmation)
            {
                ResetStationButtons();
            }
        }

        private void StopLastAudioIntroduction()
        {
            if (_lastAudioButton != null)
            {
                _lastAudioButton.ForceStop();
            }
        }

        private void ResetStationButtons()
        {
            if (_currentStationButtons is null)
            {
                return;
            }
            
            _currentStationButtons.SetAllNotFinishedCategoriesToInactive();
        }

        private void SetLastAudioMute(bool isEnabled)
        {
            if (_lastAudioButton != null)
            {
                _lastAudioButton.AudioSource.mute = !isEnabled;
            }
        }
    }
}
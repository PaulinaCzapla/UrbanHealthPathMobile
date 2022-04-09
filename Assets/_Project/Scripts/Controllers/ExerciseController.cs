using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for showing appropriate popup for given exercise.
    /// </summary>
    public class ExerciseController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;

        public ExerciseController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager)
            : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
        }

        public void ShowPopupForExercise(Exercise exercise)
        {
            _coroutineManager.StartCoroutine(CreatePopupForExerciseLevel(exercise.DisplayedName, exercise.Levels[0]));
        }

        private IEnumerator CreatePopupForExerciseLevel(string exerciseName, ExerciseLevel exerciseLevel)
        {
            yield return new WaitForEndOfFrame();
            IPopupable popupableView = ViewManager.CurrentView.GetComponent<IPopupable>();

            ClearPopup();

            switch (exerciseLevel)
            {
                case VideoExerciseLevel videoExerciseLevel:
                    OpenVideoExercisePopup(exerciseName, new PopupPayload(popupableView.PopupArea),
                        videoExerciseLevel);
                    break;
                case ImageExerciseLevel imageExerciseLevel:
                    OpenImageExerciseLevelPopup(exerciseName, new PopupPayload(popupableView.PopupArea),
                        imageExerciseLevel);
                    break;
                case ImageSelectionExerciseLevel imageSelectionExerciseLevel:
                    OpenImageSelectionExerciseLevelPopup(new PopupPayload(popupableView.PopupArea),
                        imageSelectionExerciseLevel);
                    break;
                case ImageSelectionExplanationExerciseLevel imageSelectionExplanationExerciseLevel:
                    OpenImageSelectionExplanationExerciseLevelPopup(new PopupPayload(popupableView.PopupArea),
                        imageSelectionExplanationExerciseLevel);
                    break;
                case AnswerSelectionExerciseLevel answerSelectionExerciseLevel:
                    OpenAnswerSelectionExerciseLevelPopup(new PopupPayload(popupableView.PopupArea),
                        answerSelectionExerciseLevel);
                    break;
                case TextExerciseLevel textExerciseLevel:
                    OpenTextExerciseLevelPopup(exerciseName, new PopupPayload(popupableView.PopupArea), textExerciseLevel);
                    break;
                default:
                    break;
            }
        }

        private void OpenTextExerciseLevelPopup(string exerciseName, PopupPayload popupPayload, TextExerciseLevel textExerciseLevel)
        {
            PopupManager.OpenPopup(PopupType.WithTextAndImage,
                new PopupWithTextAndImageInitializationParameters(exerciseName,
                    textExerciseLevel.Description,
                    null,
                    popupPayload));
        }

        private void OpenAnswerSelectionExerciseLevelPopup(PopupPayload popupPayload, AnswerSelectionExerciseLevel answerSelectionExerciseLevel)
        {
            List<QuizWithTextElementOption> textQuizElementOptions = new List<QuizWithTextElementOption>();

            foreach (string answer in answerSelectionExerciseLevel.Answers)
            {
                bool isCorrect =
                    answerSelectionExerciseLevel.CorrectAnswers.Contains(
                        answerSelectionExerciseLevel.Answers.IndexOf(answer));

                textQuizElementOptions.Add(new QuizWithTextElementOption(answer,
                    (button) =>
                    {
                        if (isCorrect)
                        {
                            button.EnableGreenFrame();
                        }
                        else
                        {
                            button.EnableRedFrame();
                        }
                    }));
            }

            PopupManager.OpenPopup(PopupType.QuizWithTexts,
                new QuizWithTextPopupInitializationParameters(answerSelectionExerciseLevel.Question,
                    popupPayload,
                    textQuizElementOptions.ToArray()
                ));
        }

        private void OpenImageSelectionExplanationExerciseLevelPopup(PopupPayload popupPayload, ImageSelectionExplanationExerciseLevel imageSelectionExplanationExerciseLevel)
        {
            List<Texture> images = new List<Texture>();

            foreach (LateBoundValue<MediaFile> image in imageSelectionExplanationExerciseLevel.Images)
            {
                Texture2D texture = new TextureFileAccessor(image).GetMedia();
                images.Add(texture);
            }

            PopupManager.OpenPopup(PopupType.QuizExplanationPopup,
                new QuizExplanationPopupInitializationParameters(popupPayload,
                    imageSelectionExplanationExerciseLevel.Explanations.ToArray(),
                    images.ToArray()
                ));
        }

        private void OpenImageSelectionExerciseLevelPopup(PopupPayload popupPayload, ImageSelectionExerciseLevel imageSelectionExerciseLevel)
        {
            List<QuizElementOption> imageQuizElementOptions = new List<QuizElementOption>();

            foreach (LateBoundValue<MediaFile> image in imageSelectionExerciseLevel.Images)
            {
                Texture2D texture = new TextureFileAccessor(image).GetMedia();
                bool isCorrect =
                    imageSelectionExerciseLevel.CorrectAnswers.Contains(
                        imageSelectionExerciseLevel.Images.IndexOf(image));

                imageQuizElementOptions.Add(new QuizElementOption(texture,
                    (button) =>
                    {
                        if (isCorrect)
                        {
                            button.EnableGreenFrame();
                        }
                        else
                        {
                            button.EnableRedFrame();
                        }
                    }));
            }

            PopupManager.OpenPopup(PopupType.QuizWithImages,
                new QuizWithImagesPopupInitializationParameters(imageSelectionExerciseLevel.Question,
                    popupPayload,
                    imageQuizElementOptions.ToArray()
                ));
        }

        private void OpenImageExerciseLevelPopup(string exerciseName, PopupPayload popupPayload, ImageExerciseLevel imageExerciseLevel)
        {
            PopupManager.OpenPopup(PopupType.WithTextAndImage,
                new PopupWithTextAndImageInitializationParameters(exerciseName,
                    imageExerciseLevel.Description,
                    new TextureFileAccessor(imageExerciseLevel.ImageFile).GetMedia(),
                    popupPayload));
        }

        private void OpenVideoExercisePopup(string exerciseName, PopupPayload payload, VideoExerciseLevel videoExerciseLevel)
        {
            PopupManager.OpenPopup(PopupType.WithTextAndVideo,
                new PopupWithTextAndVideoInitializationParameters(exerciseName,
                    videoExerciseLevel.Description,
                    new VideoFileAccessor(videoExerciseLevel.VideoFile).GetMedia(),
                    payload));
        }

        private void ClearPopup()
        {
            if (PopupManager.CurrentPopupType != PopupType.None)
            {
                PopupManager.CloseCurrentPopup();
            }
        }
    }
}
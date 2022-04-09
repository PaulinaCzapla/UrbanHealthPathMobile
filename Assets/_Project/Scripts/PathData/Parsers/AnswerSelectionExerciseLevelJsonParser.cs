using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into AnswerSelectionExerciseLevel.
    /// </summary>
    public class AnswerSelectionExerciseLevelJsonParser : ValidatedJsonObjectParser<AnswerSelectionExerciseLevel>
    {
        private const string QUESTION_KEY = "question";
        private const string MIN_DIFFICULTY_KEY = "min_difficulty";
        private const string MAX_DIFFICULTY_KEY = "max_difficulty";
        private const string ANSWERS_KEY = "answers";
        private const string CORRECT_ANSWERS_KEY = "correct_answers";
        
        public AnswerSelectionExerciseLevelJsonParser() : base(new[] {QUESTION_KEY, MIN_DIFFICULTY_KEY, MAX_DIFFICULTY_KEY, ANSWERS_KEY, CORRECT_ANSWERS_KEY})
        {
        }
        
        protected override AnswerSelectionExerciseLevel ParseJsonObject(JObject json)
        {
            DifficultyRange difficultyRange = new DifficultyRange(json[MIN_DIFFICULTY_KEY].Value<int>(), json[MAX_DIFFICULTY_KEY].Value<int>());
            
            List<string> answers = new List<string>();
            
            foreach (JToken answer in json[ANSWERS_KEY])
            {
                answers.Add(answer.Value<string>());
            }
            
            List<int> correctAnswers = new List<int>();

            foreach (JToken correctAnswerIndex in json[CORRECT_ANSWERS_KEY])
            {
                correctAnswers.Add(correctAnswerIndex.Value<int>());
            }

            return new AnswerSelectionExerciseLevel(difficultyRange, json[QUESTION_KEY].Value<string>(), answers, correctAnswers);
        }
    }
}
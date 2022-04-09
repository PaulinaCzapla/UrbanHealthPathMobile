using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into general ExerciseLevel.
    /// </summary>
    public class ExerciseLevelJsonParser : ValidatedJsonObjectParser<ExerciseLevel>
    {
        private const string TYPE_KEY = "type";

        private readonly IDictionary<string, IParser<JObject, ExerciseLevel>> _registeredTypesParsers;

        public ExerciseLevelJsonParser(IExerciseLevelTypesParsersFactory<JObject> exerciseLevelTypesParsersFactory) : base(new[] {TYPE_KEY})
        {
            _registeredTypesParsers = new Dictionary<string, IParser<JObject, ExerciseLevel>>();
            _registeredTypesParsers.Add("text", exerciseLevelTypesParsersFactory.CreateTextExerciseParser());
            _registeredTypesParsers.Add("video", exerciseLevelTypesParsersFactory.CreateVideoExerciseParser());
            _registeredTypesParsers.Add("image", exerciseLevelTypesParsersFactory.CreateImageExerciseParser());
            _registeredTypesParsers.Add("answer_selection", exerciseLevelTypesParsersFactory.CreateAnswerSelectionExerciseParser());
            _registeredTypesParsers.Add("image_selection", exerciseLevelTypesParsersFactory.CreateImageSelectionExerciseParser());
            _registeredTypesParsers.Add("historical_fact", exerciseLevelTypesParsersFactory.CreateHistoricalFactExerciseParser());
            _registeredTypesParsers.Add("image_selection_explanation", exerciseLevelTypesParsersFactory.CreateImageSelectionExplanationExerciseParser());
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            string parserKey = json[TYPE_KEY].Value<string>();
            
            if (!_registeredTypesParsers.ContainsKey(parserKey))
            {
                throw new ParsingException($"Parser with type {parserKey} not found!");
            }
        }

        protected override ExerciseLevel ParseJsonObject(JObject json)
        {
            return _registeredTypesParsers[json[TYPE_KEY].Value<string>()].Parse(json);
        }
    }
}
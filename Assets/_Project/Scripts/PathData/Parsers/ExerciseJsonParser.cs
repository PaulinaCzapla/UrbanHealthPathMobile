using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into Exercise.
    /// </summary>
    public class ExerciseJsonParser : ValidatedJsonObjectParser<Exercise>
    {
        private const string ID_KEY = "exercise_id";
        private const string DISPLAYED_NAME_KEY = "displayed_name";
        private const string CATEGORY_KEY = "category";
        private const string SUBCATEGORY_KEY = "subcategory";
        private const string LEVELS_KEY = "levels";
        
        private readonly JsonObjectParser<ExerciseLevel> _exerciseLevelParser;

        public ExerciseJsonParser(JsonObjectParser<ExerciseLevel> exerciseLevelParser) : base(
            new[] {ID_KEY, DISPLAYED_NAME_KEY, CATEGORY_KEY, SUBCATEGORY_KEY, LEVELS_KEY})
        {
            _exerciseLevelParser = exerciseLevelParser;
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            if (json[LEVELS_KEY].Type != JTokenType.Array)
            {
                throw new ParsingException();
            }
        }

        protected override Exercise ParseJsonObject(JObject json)
        {
            List<ExerciseLevel> exerciseLevels = new List<ExerciseLevel>();

            foreach (JObject level in json[LEVELS_KEY])
            {
                exerciseLevels.Add(_exerciseLevelParser.Parse(level));
            }

            if (!Enum.TryParse(json[CATEGORY_KEY].Value<string>(), true, out ExerciseCategory category))
            {
                throw new ParsingException();
            }
            
            if (!Enum.TryParse(json[SUBCATEGORY_KEY].Value<string>(), true, out ExerciseSubcategory subcategory))
            {
                throw new ParsingException();
            }

            return new Exercise(json[ID_KEY].Value<string>(), json[DISPLAYED_NAME_KEY].Value<string>(), category,
                subcategory, exerciseLevels);
        }
    }
}
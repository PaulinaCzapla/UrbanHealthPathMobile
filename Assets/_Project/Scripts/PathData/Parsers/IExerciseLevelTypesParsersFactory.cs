using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Interface defining factory methods for all exercise level types.
    /// </summary>
    /// <typeparam name="T">Output type of parsers.</typeparam>
    public interface IExerciseLevelTypesParsersFactory<in T>
    {
        IParser<T, TextExerciseLevel> CreateTextExerciseParser();
        IParser<T, VideoExerciseLevel> CreateVideoExerciseParser();
        IParser<T, ImageExerciseLevel> CreateImageExerciseParser();
        IParser<T, AnswerSelectionExerciseLevel> CreateAnswerSelectionExerciseParser();
        IParser<T, ImageSelectionExerciseLevel> CreateImageSelectionExerciseParser();
        IParser<T, HistoricalFactExerciseLevel> CreateHistoricalFactExerciseParser();
        IParser<T, ImageSelectionExplanationExerciseLevel> CreateImageSelectionExplanationExerciseParser();
    }
}
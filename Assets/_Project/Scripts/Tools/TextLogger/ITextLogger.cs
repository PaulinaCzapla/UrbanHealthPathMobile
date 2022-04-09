namespace PolSl.UrbanHealthPath.Tools.TextLogger
{
    /// <summary>
    /// Interface defining methods for logging a text message.
    /// </summary>
    public interface ITextLogger
    {
        void Log(LogVerbosity verbosity, string message);
        void Log(LogVerbosity verbosity, string category, string message);
    }
}
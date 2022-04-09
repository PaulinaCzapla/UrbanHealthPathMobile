namespace PolSl.UrbanHealthPath.Tools.TextLogger
{
    /// <summary>
    /// Dummy implementation of ITextLogger that does nothing.
    /// </summary>
    public class DummyLogger : ITextLogger
    {
        public void Log(LogVerbosity verbosity, string message)
        {
        }

        public void Log(LogVerbosity verbosity, string category, string message)
        {
        }
    }
}
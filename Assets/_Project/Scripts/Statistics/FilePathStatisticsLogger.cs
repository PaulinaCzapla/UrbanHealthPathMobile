using System;
using System.IO;
using System.Security;
using PolSl.UrbanHealthPath.Tools.TextLogger;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Base class for all path statistics loggers that log to files.
    /// </summary>
    public abstract class FilePathStatisticsLogger : IPathStatisticsLogger
    {
        private readonly ITextLogger _textLogger;
        private readonly string _pathToFile;
        private readonly bool _shouldAppendData;

        public FilePathStatisticsLogger(ITextLogger textLogger, string pathToFile, bool shouldAppendData = false)
        {
            _textLogger = textLogger;
            _pathToFile = pathToFile;
            _shouldAppendData = shouldAppendData;
        }

        public void LogCompletedPathStatistics(PathStatistics pathStatistics)
        {
            LogToFile(BuildFileContentFromCompletedPathStatistics(pathStatistics));
        }

        public void LogCancelledPathStatistics(PathStatistics pathStatistics)
        {
            LogToFile(BuildFileContentFromCancelledPathStatistics(pathStatistics));
        }

        protected abstract string BuildFileContentFromCompletedPathStatistics(PathStatistics pathStatistics);
        protected abstract string BuildFileContentFromCancelledPathStatistics(PathStatistics pathStatistics);

        private void LogToFile(string content)
        {
            try
            {
                StreamWriter writer = GetNewStreamWriter();
                writer.Write(content);
                writer.Close();
            }
            catch (Exception ex) when (ex is IOException || ex is SecurityException)
            {
                _textLogger.Log(LogVerbosity.Error, $"Could not log path statistics to file! {ex.Message}");
            }
        }
        
        private StreamWriter GetNewStreamWriter()
        {
            return new StreamWriter(_pathToFile, _shouldAppendData);
        }
    }
}
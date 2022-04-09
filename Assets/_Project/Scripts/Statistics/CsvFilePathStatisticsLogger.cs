using System;
using System.Collections.Generic;
using System.Globalization;
using PolSl.UrbanHealthPath.Tools.TextLogger;

namespace PolSl.UrbanHealthPath.Statistics
{
    /// <summary>
    /// Path statistics logger that log to CSV-formatted file.
    /// </summary>
    public class CsvFilePathStatisticsLogger : FilePathStatisticsLogger
    {
        private const string FIELD_SEPARATOR = ","; 
            
        private readonly List<string> _fields;
        
        public CsvFilePathStatisticsLogger(ITextLogger logger, string pathToFile) : base(logger, pathToFile, true)
        {
            _fields = new List<string>();
        }

        protected override string BuildFileContentFromCompletedPathStatistics(PathStatistics pathStatistics)
        {
            ResetFields();
            AddField("true");
            BuildCommonFields(pathStatistics);
            return GetRow();
        }

        protected override string BuildFileContentFromCancelledPathStatistics(PathStatistics pathStatistics)
        {
            ResetFields();
            AddField("false");
            BuildCommonFields(pathStatistics);
            return GetRow();
        }

        private void BuildCommonFields(PathStatistics pathStatistics)
        {
            AddField(pathStatistics.PathId);
            AddField(pathStatistics.FinishedAt.ToString(CultureInfo.InvariantCulture));
            AddField(pathStatistics.VisitedPointsCount.ToString());
            AddField(pathStatistics.EstimatedDistance.ToString());
        }

        private string GetRow()
        {
            return string.Join(FIELD_SEPARATOR, _fields) + Environment.NewLine;
        }

        private void AddField(string fieldContent)
        {
            _fields.Add(fieldContent);
        }

        private void ResetFields()
        {
            _fields.Clear();
        }
    }
}
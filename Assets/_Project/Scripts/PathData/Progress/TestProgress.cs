using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.TestData;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class TestProgress
    {
        public List<TestExerciseSummary> ExerciseSummaries { get; }
        public DateTime TestDate { get; }
        
        public TestProgress(List<TestExerciseSummary> exerciseSummaries, DateTime testDate)
        {
            ExerciseSummaries = exerciseSummaries;
            TestDate = testDate;
        }

        public TestProgress()
        {
            ExerciseSummaries = new List<TestExerciseSummary>();
            TestDate = DateTime.Now;
        }

        public void AddNewSummary(TestExerciseSummary summary)
        {
            ExerciseSummaries.Add(summary);
        }

        public int GetFinishedExercisesCount()
        {
            return ExerciseSummaries.Count;
        }
    }
}

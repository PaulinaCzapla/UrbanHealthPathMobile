using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.TestData
{
    public struct TestExerciseSummary
    {
        public string ExerciseId { get; }
        public int ExerciseTimeInSeconds { get; }

        public TestExerciseSummary(string exerciseId, int exerciseTimeInSeconds)
        {
            ExerciseId = exerciseId;
            ExerciseTimeInSeconds = exerciseTimeInSeconds;
        }
    }
}

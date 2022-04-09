using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class Test 
    {
        public string TestId { get; }
        public IList<LateBoundValue<Exercise>> Exercises { get; }
        
        public Test(string testId, IList<LateBoundValue<Exercise>> exercises)
        {
            TestId = testId;
            Exercises = exercises;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface ITestLoader 
    {
        IList<Test> LoadTests();
    }
}

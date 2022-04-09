using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Enum representing a state of a path.
    /// </summary>
    [Serializable]
    public enum PathState
    {
        InProgress,
        Completed,
        Cancelled
    }
}
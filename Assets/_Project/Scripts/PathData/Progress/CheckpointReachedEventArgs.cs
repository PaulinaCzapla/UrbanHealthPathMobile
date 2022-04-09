using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Arguments for CheckpointReached event.
    /// </summary>
    public class CheckpointReachedEventArgs : EventArgs
    {
        public PathProgressCheckpoint Checkpoint { get; }

        public CheckpointReachedEventArgs(PathProgressCheckpoint checkpoint)
        {
            Checkpoint = checkpoint;
        }
    }
}
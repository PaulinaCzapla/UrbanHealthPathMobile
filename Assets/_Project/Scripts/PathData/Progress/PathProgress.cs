using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    /// <summary>
    /// Class representing path progress.
    /// </summary>
    public class PathProgress
    {
        [JsonProperty] private IList<PathProgressCheckpoint> _progressCheckpoints;
        
        public DateTime StartedAt { get; }
        public PathState State { get; private set; }
        [JsonIgnore] public PathProgressCheckpoint LastCheckpoint => _progressCheckpoints.LastOrDefault();
        [JsonIgnore] public bool WasFinished => State == PathState.Completed || State == PathState.Cancelled;

        public PathProgress(DateTime startedAt, PathState state, IList<PathProgressCheckpoint> progressCheckpoints)
        {
            StartedAt = startedAt;
            State = state;
            _progressCheckpoints = progressCheckpoints;
        }

        public bool AddCheckpoint(PathProgressCheckpoint checkpoint)
        {
            if (WasFinished)
            {
                return false;
            }
            
            _progressCheckpoints.Add(checkpoint);
            return true;
        }

        public void CompletePath()
        {
            if (WasFinished)
            {
                return;
            }
            
            State = PathState.Completed;
        }

        public void CancelPath()
        {
            if (WasFinished)
            {
                return;
            }
            
            State = PathState.Cancelled;
        }
    }
}
using System;
using System.Collections;
using Newtonsoft.Json;
using PolSl.UrbanHealthPath.PathData.Progress;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class PathProgressSimulator : MonoBehaviour
    {
        [SerializeField] private string[] _waypointsForCheckpointsToAdd;
        [SerializeField] private int _delayBeforeStart;
        [SerializeField] private int _delayBetweenAdding;

        private IPathProgressManager _progressManager;

        private int _reachedCheckpointsCounter = 0;

        private void Start()
        {
            _progressManager =
                new PathProgressManager(
                    new JsonFilePathProgressPersistor(Application.temporaryCachePath + "/testSave.json",
                        new JsonSerializer()));
            _progressManager.CheckpointReached += CheckWasLastCheckpoint;
            
            _progressManager.StartNewPath();
            StartCoroutine(AutoAddCheckpoints());
        }

        private void OnDestroy()
        {
            _progressManager.CheckpointReached -= CheckWasLastCheckpoint;
        }

        private IEnumerator AutoAddCheckpoints()
        {
            yield return new WaitForSeconds(_delayBeforeStart);

            foreach (string waypoint in _waypointsForCheckpointsToAdd)
            {
                AddNextCheckpoint(new PathProgressCheckpoint(waypoint, DateTime.Now));
                yield return new WaitForSeconds(_delayBetweenAdding);
            }
        }

        private void AddNextCheckpoint(PathProgressCheckpoint checkpoint)
        {
            _progressManager.AddCheckpoint(checkpoint);
        }

        private void CheckWasLastCheckpoint(object sender, EventArgs e)
        {
            _reachedCheckpointsCounter++;

            if (_reachedCheckpointsCounter == _waypointsForCheckpointsToAdd.Length)
            {
                _progressManager.CancelPath();
            }
        }
    }
}
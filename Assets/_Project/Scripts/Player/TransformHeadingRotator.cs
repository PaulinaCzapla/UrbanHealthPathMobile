using Mapbox.Unity.Location;
using PolSl.UrbanHealthPath.Map;
using UnityEngine;


namespace PolSl.UrbanHealthPath.Player
{
    /// <summary>
    /// Changes the heading of the player to the one polled from the updated location. 
    /// </summary>
    public class TransformHeadingRotator : MonoBehaviour
    {
        private ILocationUpdater _locationUpdater;
        private Quaternion _targetRotation;
        private bool _initialized;

        public void Initialize(ILocationUpdater locationUpdater)
        {
            _locationUpdater = locationUpdater;
            _locationUpdater.LocationUpdated += RecalculateTargetRotation;
            _initialized = true;
        }

        private void Update()
        {
            if (_initialized)
            {
                transform.localRotation =
                    Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime * 2f);
            }
        }

        private void OnDestroy()
        {
            if (_initialized)
            {
                _locationUpdater.LocationUpdated -= RecalculateTargetRotation;
            }
        }

        void RecalculateTargetRotation(LocationUpdatedArgs args)
        {
            Location location = args.Location;
            
            float rotationAngle = location.UserHeading * -1f;
            if (location.IsUserHeadingUpdated)
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(rotationAngle));
            }
        }

        private Vector3 GetNewEulerAngles(float newAngle)
        {
            Quaternion localRotation = transform.localRotation;
            Vector3 currentEuler = localRotation.eulerAngles;
            Vector3 euler = Mapbox.Unity.Constants.Math.Vector3Zero;
            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;
            return euler;
        }
    }
}

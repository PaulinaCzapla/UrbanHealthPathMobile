using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEditor;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Location Provider which updates the location pulled from the device's GPS
    /// </summary>
    public class DeviceLocationProvider : ILocationProvider
    {
        private float _desiredAccuracyInMeters = 1.0f;

        private float _updateDistanceInMeters = 0.0f;

        private System.Globalization.CultureInfo invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
        
        private AngleSmootherLowPas _deviceOrientationSmoothing; 

        private Location _currentLocation;

        private IMapboxLocationService _locationService;

        private Vector2d _lastPosition;

        private double _lastLocationTimeStamp;
        
        public DeviceLocationProvider()
        {
            _deviceOrientationSmoothing = new AngleSmootherLowPas();
            _locationService = new MapboxLocationServiceUnityWrapper();
            _currentLocation.Provider = "unity";
            _currentLocation.IsLocationServiceEnabled = true;
            _locationService.Start(_desiredAccuracyInMeters, _updateDistanceInMeters);
            Input.compass.enabled = true;
        }

        public Location GetLocation()
        {
            PollLocation();
            return _currentLocation;
        }
        
        private void PollLocation()
        {
            IMapboxLocationInfo lastData = _locationService.lastData;
            _currentLocation.IsLocationServiceEnabled = _locationService.status == LocationServiceStatus.Running ||
                                                        lastData.timestamp > _lastLocationTimeStamp;
            _deviceOrientationSmoothing.Add(Input.compass.trueHeading);
            _currentLocation.UserHeading = (float) _deviceOrientationSmoothing.Calculate();
            _currentLocation.IsUserHeadingUpdated = true;
            double latitude = double.Parse(lastData.latitude.ToString("R", invariantCulture), invariantCulture);
            double longitude = double.Parse(lastData.longitude.ToString("R", invariantCulture), invariantCulture);
            _lastPosition = _currentLocation.LatitudeLongitude;
            _currentLocation.LatitudeLongitude = new Vector2d(latitude, longitude);
            _currentLocation.Accuracy = (float) Math.Floor(lastData.horizontalAccuracy);
            _currentLocation.Timestamp = lastData.timestamp;
            _currentLocation.IsLocationUpdated = _currentLocation.Timestamp > _lastLocationTimeStamp ||
                                                 !_currentLocation.LatitudeLongitude.Equals(_lastPosition);
            _lastLocationTimeStamp = _currentLocation.Timestamp;
        }
    }
}

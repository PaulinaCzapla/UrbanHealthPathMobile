using System;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using PolSl.UrbanHealthPath.PathData;


namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Location Provider which updates the location of the user to one of the Locations stored in the
    /// _latitudeLongitud list
    /// </summary>
    public class FakeLocationProvider : ILocationProvider
    {
        private float _userHeading = 0;

        private int _accuracy = 5;
        
        private List<Coordinates> _latitudeLongitude;
        
        private Location _currentLocation;

        private int _index = -1;
        
        public FakeLocationProvider(List<Coordinates> latitudeLongitude)
        {
            if (latitudeLongitude != null && latitudeLongitude.Count!=0)
            {
                _latitudeLongitude = latitudeLongitude;
            }
            else
            {
                _latitudeLongitude = new List<Coordinates> {new Coordinates(50.29416712031348, 18.665418882944635)};//neptun
            }
        }
        
        public Location GetLocation()
        {
            PollLocation();
            return _currentLocation;
        }
        
        private void PollLocation()
        {
            _index++;
            if (_index >= _latitudeLongitude.Count)
            {
                _index = 0;
            }
            _currentLocation.LatitudeLongitude = _latitudeLongitude[_index];
            _currentLocation.Timestamp = UnixTimestampUtils.To(DateTime.UtcNow);
            _currentLocation.IsLocationUpdated = true;
            _currentLocation.IsUserHeadingUpdated = true;
            _currentLocation.UserHeading = _userHeading;
            _currentLocation.Accuracy = _accuracy;
        }
    }
}

namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    /// <summary>
    /// Class that represents a real-world distance.
    /// </summary>
    public readonly struct Distance : IDistance
    {
        private const double METERS_IN_KILOMETER = 1000;
        
        private readonly double _distanceInMeters;
        
        private Distance(double distanceInMeters)
        {
            _distanceInMeters = distanceInMeters;
        }

        public double InKilometers()
        {
            return _distanceInMeters / METERS_IN_KILOMETER;
        }

        public double InMeters()
        {
            return _distanceInMeters;
        }

        public bool LessThan(IDistance comparedDistance)
        {
            return _distanceInMeters < comparedDistance.InMeters();
        }

        public bool GreaterThan(IDistance comparedDistance)
        {
            return _distanceInMeters > comparedDistance.InMeters();
        }

        public static Distance FromKilometers(double kilometers)
        {
            return new Distance(kilometers * METERS_IN_KILOMETER);
        }

        public static Distance FromMeters(double meters)
        {
            return new Distance(meters);
        }
    }
}
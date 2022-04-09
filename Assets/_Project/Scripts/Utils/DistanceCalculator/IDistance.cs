namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    /// <summary>
    /// Interface representing methods related to conversion and comparision of distances.
    /// </summary>
    public interface IDistance
    {
        double InKilometers();
        double InMeters();

        bool LessThan(IDistance comparedDistance);
        bool GreaterThan(IDistance comparedDistance);
    }
}
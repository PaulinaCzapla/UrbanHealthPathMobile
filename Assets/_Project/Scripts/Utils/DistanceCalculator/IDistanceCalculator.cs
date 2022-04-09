using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    /// <summary>
    /// Interface that defines a method for calculating a linear distance between two coordinates.
    /// </summary>
    public interface IDistanceCalculator
    {
        IDistance CalculateDistance(Coordinates from, Coordinates to);
    }
}
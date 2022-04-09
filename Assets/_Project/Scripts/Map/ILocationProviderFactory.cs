using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Interface which implementaitons create instances of LocationUpdater
    /// </summary>
    public interface ILocationProviderFactory
    {
        ILocationProvider CreateProvider();
        ILocationProvider CreateProvider(List<Coordinates> coordinatesList);
    }
}
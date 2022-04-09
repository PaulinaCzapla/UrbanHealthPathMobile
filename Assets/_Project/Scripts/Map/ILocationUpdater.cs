using System;
using System.Collections;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Interface which implementations update the location of the user.
    /// </summary>
    public interface ILocationUpdater
    {
        event Action<LocationUpdatedArgs> LocationUpdated;
        IEnumerator UpdateLocation();
    }
}
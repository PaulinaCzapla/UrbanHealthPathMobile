using UnityEngine;
namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
    /// <summary>
    /// Base class of event channels implemented as ScriptableObjects.
    /// </summary>
    public class EventChannelBaseSO: ScriptableObject
    {
        [TextArea] public string description;
    }
}
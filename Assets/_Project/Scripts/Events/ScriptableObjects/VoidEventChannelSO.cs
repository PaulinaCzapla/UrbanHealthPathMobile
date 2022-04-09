using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
    /// <summary>
    /// Channel for events with no arguments.
    /// </summary>
    [CreateAssetMenu(menuName = "UrbanHealthPath/Events/Void Event Channel")]
    public class VoidEventChannelSO : EventChannelBaseSO
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
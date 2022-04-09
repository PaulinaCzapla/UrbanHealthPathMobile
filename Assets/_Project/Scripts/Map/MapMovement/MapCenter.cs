using System;
using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.Map.MapMovement
{
    /// <summary>
    /// A class that represents map center feature.
    /// </summary>
    public class MapCenter : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _onMapCentered;
        [SerializeField] private Button _centerButton;

        private void OnEnable()
        {
            _centerButton.onClick.AddListener(() => _onMapCentered.RaiseEvent());
        }

        private void OnDisable()
        {
            _centerButton.onClick.RemoveAllListeners();
        }
    }
}
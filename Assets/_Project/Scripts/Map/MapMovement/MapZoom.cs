using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.Map.MapMovement
{
    /// <summary>
    /// A class that receives inputs and invokes zoom in/ zoom out events.
    /// </summary>
    public class MapZoom : MonoBehaviour
    {
        [SerializeField] private Button _zoomIn;
        [SerializeField] private Button _zoomOut;

        [SerializeField] private VoidEventChannelSO _onMapZoomOut;
        [SerializeField] private VoidEventChannelSO _onMapZoomIn;
        [SerializeField] private VoidEventChannelSO _onMaxZoomInAchieved;
        [SerializeField] private VoidEventChannelSO _onMaxZoomOutAchieved;

        private void OnEnable()
        {
            _onMaxZoomInAchieved.OnEventRaised += OnMaxZoomIn;
            _onMaxZoomOutAchieved.OnEventRaised += OnMaxZoomOut;
            _zoomIn.onClick.AddListener(ZoomInClicked);
            _zoomOut.onClick.AddListener(ZoomOutClicked);
            _zoomIn.interactable = false;
        }

        private void OnDisable()
        {
            _onMaxZoomInAchieved.OnEventRaised -= OnMaxZoomIn;
            _onMaxZoomOutAchieved.OnEventRaised -= OnMaxZoomOut;
            _zoomIn.onClick.RemoveListener(ZoomInClicked);
            _zoomOut.onClick.RemoveListener(ZoomOutClicked);
        }

        private void OnMaxZoomIn()
        {
            _zoomIn.interactable = false;
            _zoomOut.interactable = true;
        }

        private void OnMaxZoomOut()
        {
            _zoomIn.interactable = true;
            _zoomOut.interactable = false;
        }
        private void ZoomInClicked()
        {
            _onMapZoomIn.RaiseEvent();
            _zoomOut.interactable = true;
        }

        private void ZoomOutClicked()
        {
            _onMapZoomOut.RaiseEvent();
            _zoomIn.interactable = true;
        }
    }
}
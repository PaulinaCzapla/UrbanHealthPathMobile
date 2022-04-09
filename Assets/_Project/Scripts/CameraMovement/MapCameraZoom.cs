using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;

namespace PolSl.UrbanHealthPath.CameraMovement
{
    /// <summary>
    /// A class that is responsible for camera zoom, that depends on player's input.
    /// </summary>
    public class MapCameraZoom : MonoBehaviour
    {
        [Header("Event channels")] [SerializeField]
        private VoidEventChannelSO _onMapZoomOut;

        [SerializeField] private VoidEventChannelSO _onMapZoomIn;
        [SerializeField] private VoidEventChannelSO _onMaxZoomInAchieved;
        [SerializeField] private VoidEventChannelSO _onMaxZoomOutAchieved;


        [Header("Variables")] [SerializeField] private UnityEngine.Camera _cam;
        [SerializeField] private float _initialZoom = 50;
        [SerializeField] private int _maxZoomCount = 3;
        [SerializeField] private float _zoomDelta = 75;

        private int _index;
        private float _currentZoom;

        private void OnEnable()
        {
            _currentZoom = _initialZoom;
            _onMapZoomOut.OnEventRaised += ZoomOut;
            _onMapZoomIn.OnEventRaised += ZoomIn;
        }

        private void OnDisable()
        {
            _onMapZoomOut.OnEventRaised -= ZoomOut;
            _onMapZoomIn.OnEventRaised -= ZoomIn;
        }

        public void SetInitialZoom()
        {
            _index = 0;
            _currentZoom = _initialZoom;
            _onMaxZoomInAchieved.RaiseEvent();

            StartCoroutine(ZoomLerp(_initialZoom, 0.5f));
        }

        private void ZoomOut()
        {
            _index++;

            if (_index > _maxZoomCount - 1)
            {
                _index--;
                return;
            }

            if (_index == _maxZoomCount - 1)
                _onMaxZoomOutAchieved.RaiseEvent();

            _currentZoom = _currentZoom + _zoomDelta;

            StartCoroutine(ZoomLerp(_currentZoom, 0.5f));
        }

        private void ZoomIn()
        {
            _index--;

            if (_index < 0)
            {
                _index = 0;
                return;
            }

            if (_index == 0)
                _onMaxZoomInAchieved.RaiseEvent();

            _currentZoom = _currentZoom - _zoomDelta;

            StartCoroutine(ZoomLerp(_currentZoom, 0.5f));
        }

        IEnumerator ZoomLerp(float endValue, float duration)
        {
            float time = 0;
            Vector3 startValue = _cam.transform.position;

            while (time < duration)
            {
                float t = time / duration;
                t = t * t * (3f - 2f * t);

                _cam.transform.position = Vector3.Lerp(
                    new Vector3(_cam.transform.position.x, startValue.y, _cam.transform.position.z),
                    new Vector3(_cam.transform.position.x, endValue, _cam.transform.position.z), t);

                time += Time.deltaTime;
                yield return null;
            }

            _cam.transform.position = new Vector3(_cam.transform.position.x, endValue, _cam.transform.position.z);
        }
    }
}
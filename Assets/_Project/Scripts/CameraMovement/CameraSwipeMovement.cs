using System;
using System.Collections;
using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using PolSl.UrbanHealthPath.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.CameraMovement
{
    /// <summary>
    /// A class that is responsible for camera movement, that depends on map swipe.
    /// </summary>
    public class CameraSwipeMovement : MonoBehaviour
    {
        [SerializeField] private Vector3EventChannelSO _onMapSwiped;
        [SerializeField] private VoidEventChannelSO _onMapCentered;
        [SerializeField] private MapCameraZoom _cameraZoom;
        [SerializeField] private UnityEngine.Camera _cam;
        [SerializeField] private CameraPlayerFollower _playerFollower;
        [SerializeField] private CameraRotation _cameraRotation;

        private Vector3 _desiredPosition, _startPosition;
        private bool _shouldMove;
        private CameraRotation _rotation;

        private void Awake()
        {
            _rotation = GetComponent<CameraRotation>();
        }

        private void OnEnable()
        {
            _onMapCentered.OnEventRaised += CenterMap;
            _onMapSwiped.OnEventRaised += MoveCamera;
        }


        private void OnDisable()
        {
            _onMapCentered.OnEventRaised -= CenterMap;
            _onMapSwiped.OnEventRaised -= MoveCamera;
        }

        private void CenterMap()
        {
            _cameraZoom.SetInitialZoom();
            _shouldMove = false;
            _playerFollower.enabled = true;
            _cameraRotation.enabled = true;
        }

        /// <summary>
        /// Calculates desired position of the camera.
        /// </summary>
        private void MoveCamera(Vector3 pos)
        {
            _playerFollower.enabled = false;
            _cameraRotation.enabled = false;
            _startPosition = _cam.transform.position;

            if (_rotation.CurrentRotation.eulerAngles == new Vector3(0,180,0))
            {
                pos = pos * -1;
            } else if (_rotation.CurrentRotation.eulerAngles == new Vector3(0,90,0))
            {
                pos = new Vector3( pos.z, pos.y, pos.x * -1);
                
            }else if (_rotation.CurrentRotation.eulerAngles == new Vector3(0,270,0))
            {
                pos = new Vector3(-1 * pos.z, pos.y, pos.x);
            }
            _desiredPosition = new Vector3(_startPosition.x + pos.x, _cam.transform.position.y,
                _startPosition.z + pos.z);
            _shouldMove = true;
        }

        private void FixedUpdate()
        {
            if (Mathf.Approximately(Vector3.Distance(_cam.transform.position, _desiredPosition), 0))
                _shouldMove = false;

            if (_shouldMove)
                _cam.transform.position = Vector3.Lerp(
                    new Vector3(_startPosition.x, _cam.transform.position.y, _startPosition.z),
                    new Vector3(_desiredPosition.x, _cam.transform.position.y, _desiredPosition.z), 0.01f);
        }
    }
}
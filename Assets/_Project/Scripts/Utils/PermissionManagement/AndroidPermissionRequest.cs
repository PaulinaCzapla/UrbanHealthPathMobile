using UnityEngine.Android;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Permission request for Android device.
    /// </summary>
    public class AndroidPermissionRequest : IPermissionRequest
    {
        private readonly string _permissionName;
        private readonly PermissionCallbacks _callbacks;

        public RequestResult Result { get; private set; }

        public AndroidPermissionRequest(string permissionName)
        {
            _permissionName = permissionName;
            _callbacks = new PermissionCallbacks();
            _callbacks.PermissionGranted += OnPermissionGranted;
            _callbacks.PermissionDenied += OnPermissionDenied;
            _callbacks.PermissionDeniedAndDontAskAgain += OnPermissionDeniedAndDontAskAgain;
        }
        
        public void Request()
        {
            UnityEngine.Android.Permission.RequestUserPermission(_permissionName, _callbacks);
        }

        private void OnPermissionGranted(string permissionName)
        {
            OnPermissionRequestFinished(RequestResult.Granted);
        }

        private void OnPermissionDenied(string permissionName)
        {
            OnPermissionRequestFinished(RequestResult.Denied);
        }

        private void OnPermissionDeniedAndDontAskAgain(string permissionName)
        {
            OnPermissionRequestFinished(RequestResult.DeniedForever);
        }

        private void OnPermissionRequestFinished(RequestResult result)
        {
            UnsubscribeCallbacks();
            Result = result;
        }

        private void UnsubscribeCallbacks()
        {
            _callbacks.PermissionGranted -= OnPermissionGranted;
            _callbacks.PermissionDenied -= OnPermissionDenied;
            _callbacks.PermissionDeniedAndDontAskAgain -= OnPermissionDeniedAndDontAskAgain;
        }
    }
}
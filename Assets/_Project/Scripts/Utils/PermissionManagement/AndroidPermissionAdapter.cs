using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Permission adapter for Android device.
    /// </summary>
    public class AndroidPermissionAdapter : IPermissionAdapter
    {
        private readonly Dictionary<Permission, string> _permissionNames;

        public AndroidPermissionAdapter()
        {
            _permissionNames = new Dictionary<Permission, string>();
            SetSupportedPermissionNames();
        }
        
        public IPermissionRequest CreateRequest(Permission permission)
        {
            bool isPermissionSupported = SupportsPermission(permission);
            IPermissionRequest permissionRequest;

            if (isPermissionSupported)
            {
                permissionRequest = new AndroidPermissionRequest(GetPermissionName(permission));
            }
            else
            {
                permissionRequest = new DummyPermissionRequest();
            }

            return permissionRequest;
        }

        public bool HasPermission(Permission permission)
        {
            if (!SupportsPermission(permission))
            {
                return true;
            }

            return UnityEngine.Android.Permission.HasUserAuthorizedPermission(GetPermissionName(permission));
        }

        private void SetSupportedPermissionNames()
        {
            _permissionNames[Permission.Location] = UnityEngine.Android.Permission.FineLocation;
        }

        private bool SupportsPermission(Permission permission)
        {
            return _permissionNames.ContainsKey(permission);
        }
        
        private string GetPermissionName(Permission permission)
        {
            return _permissionNames[permission];
        }
    }
}
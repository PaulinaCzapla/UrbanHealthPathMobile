using System;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Interface defining methods for managing device permissions.
    /// </summary>
    public interface IPermissionManager
    {
        void RequirePermission(Permission permissionRequest, Action<RequestResult> resultHandler);
    }
}
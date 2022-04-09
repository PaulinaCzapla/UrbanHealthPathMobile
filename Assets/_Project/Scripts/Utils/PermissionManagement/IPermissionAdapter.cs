namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Interface defining methods for permission adapter, system specific.
    /// </summary>
    public interface IPermissionAdapter
    {
        IPermissionRequest CreateRequest(Permission permission);
        bool HasPermission(Permission permission);
    }
}
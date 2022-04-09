namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Interface representing request of device permission.
    /// </summary>
    public interface IPermissionRequest
    {
        RequestResult Result { get; }
        
        void Request();
    }
}
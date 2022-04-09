namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Dummy implementation of permission request that does nothing.
    /// </summary>
    public class DummyPermissionRequest : IPermissionRequest
    {
        public RequestResult Result { get; private set; }

        public void Request()
        {
            Result = RequestResult.Granted;
        }
    }
}
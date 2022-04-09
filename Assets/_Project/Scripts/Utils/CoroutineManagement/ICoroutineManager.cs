using System.Collections;

namespace PolSl.UrbanHealthPath.Utils.CoroutineManagement
{
    /// <summary>
    /// Interface that defines methods for starting and ending a coroutine.
    /// </summary>
    public interface ICoroutineManager
    {
        void BeginCoroutine(IEnumerator coroutine);
        void EndCoroutine(IEnumerator coroutine);
    }
}
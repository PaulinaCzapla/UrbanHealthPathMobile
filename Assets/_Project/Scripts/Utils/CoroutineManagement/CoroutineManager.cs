using System.Collections;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.CoroutineManagement
{
    /// <summary>
    /// Implementation of coroutine manager as a GameObject's component.
    /// </summary>
    public class CoroutineManager : MonoBehaviour, ICoroutineManager
    {
        public void BeginCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public void EndCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}

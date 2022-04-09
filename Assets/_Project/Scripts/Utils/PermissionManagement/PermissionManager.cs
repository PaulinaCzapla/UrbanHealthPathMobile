using System;
using System.Collections;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    /// <summary>
    /// Implementation of permission manager that uses coroutines for checking requests results.
    /// </summary>
    public class PermissionManager : IPermissionManager
    {
        private readonly IPermissionAdapter _permissionAdapter;
        private readonly ICoroutineManager _coroutineManager;

        public PermissionManager(IPermissionAdapter permissionAdapter, ICoroutineManager coroutineManager)
        {
            _permissionAdapter = permissionAdapter;
            _coroutineManager = coroutineManager;
        }

        public void RequirePermission(Permission permission, Action<RequestResult> resultHandler)
        {
            if (_permissionAdapter.HasPermission(permission))
            {
                resultHandler?.Invoke(RequestResult.Granted);
                return;
            }

            RequestPermission(permission, resultHandler);
        }

        private void RequestPermission(Permission permission, Action<RequestResult> resultHandler)
        {
            IPermissionRequest request = _permissionAdapter.CreateRequest(permission);
            request.Request();
            
            _coroutineManager.BeginCoroutine(CheckRequestResult(request, resultHandler));
        }

        private IEnumerator CheckRequestResult(IPermissionRequest request, Action<RequestResult> resultHandler)
        {
            yield return new WaitWhile(() => request.Result == RequestResult.Unknown);
            
            resultHandler?.Invoke(request.Result);
        }
    }
}
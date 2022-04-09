using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A struct that represents view. It contains ViewType and view GameObject.
    /// </summary>
    [Serializable] public struct View
    {
        [FormerlySerializedAs("type")] [SerializeField] private ViewType _type;
        [FormerlySerializedAs("viewObject")] [SerializeField] private GameObject _viewObject;

        public ViewType GetViewType()
        {
            return _type;
        }

        public GameObject GetViewObject()
        {
            return _viewObject;
        }
    }
}

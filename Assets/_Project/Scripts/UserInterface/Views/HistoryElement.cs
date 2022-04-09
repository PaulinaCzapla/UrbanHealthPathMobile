using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    /// <summary>
    /// A struct that represents History element. It contains ViewType and initializer.
    /// </summary>
    public struct HistoryElement
    {
        public ViewType Type { get; }
        public IViewInitializationParameters Data { get; }

        public HistoryElement(ViewType type, IViewInitializationParameters data)
        {
            Type = type;
            Data = data;
        }
    }
}

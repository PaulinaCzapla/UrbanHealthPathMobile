using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for HelpView.
    /// </summary>
    public class HelpViewInitializationParameters : IViewInitializationParameters
    {
        public List<ListElement> Elements { get; }
        public UnityAction ReturnEvent { get; }

        public HelpViewInitializationParameters(List<ListElement> elements, UnityAction returnEvent)
        {
            Elements = elements;
            ReturnEvent += returnEvent;
        }
    }
}
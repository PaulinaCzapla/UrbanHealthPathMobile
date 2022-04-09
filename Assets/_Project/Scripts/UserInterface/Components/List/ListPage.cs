using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    /// <summary>
    /// Represents a UI list page. Contains list elements.
    /// </summary>
    public class ListPage : MonoBehaviour
    {
        [FormerlySerializedAs("panelElements")] [SerializeField] private List<ListPanelElement> _panelElements = new List<ListPanelElement>();

        private const int MaxElementsCount = 3;

        private void Awake()
        {
            foreach (ListPanelElement panelElem in _panelElements)
            {
                if (panelElem.gameObject.activeSelf)
                    panelElem.gameObject.SetActive(false);
            }
        }

        public void Initialize(List<ListElement> elements)
        {
            for (int i = 0; i < MaxElementsCount; i++)
            {
                if (i < elements.Count)
                {
                    ListElement element = elements[i];
                    ListPanelElement panelElem = _panelElements[i];

                    panelElem.gameObject.SetActive(true);
                    panelElem.SetValues(element.ButtonText, element.IconText, element.Action, element.Icon, element.IconColor);
                }
            }
        }
    }
}
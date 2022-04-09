using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    /// <summary>
    /// Represents a UI list element. It is used for ListPanel initialization.
    /// </summary>
    public class ListElement
    {
        public string ButtonText { get; }
        public Texture2D Icon { get; }
        public Color IconColor { get; }
        public string IconText { get; }
        public UnityAction Action { get; }

        public ListElement(string buttonText, Texture2D icon, string iconText, UnityAction action)
        {
            ButtonText = buttonText ?? "";
            Icon = icon;
            IconColor = Color.black;
            IconText = iconText ?? "";
            Action += action;
        }

        public ListElement(string buttonText, Texture2D icon, Color iconColor, string iconText, UnityAction action) :
            this(buttonText, icon, iconText, action)
        {
            IconColor = iconColor;
        }
    }
}
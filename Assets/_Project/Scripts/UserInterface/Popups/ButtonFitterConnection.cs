using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// Struct that contains a button and it's image fitter.
    /// </summary>
    [Serializable] public struct ButtonFitterConnection
    {
        [SerializeField] private ImageFitter _imageFitter;

        [SerializeField] private Button _button;

        public ImageFitter ImageFitter => _imageFitter;

        public Button Button => _button;

        public ButtonFitterConnection(ImageFitter imageFitter, Button button)
        {
            _imageFitter = imageFitter;
            _button = button;
        }
    }
}

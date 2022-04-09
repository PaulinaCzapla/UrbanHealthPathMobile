using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// Represents a UI header.
    /// </summary>
    public class HeaderPanel : MonoBehaviour
    {
        [FormerlySerializedAs("text")] [SerializeField] private TextMeshProUGUI _text;
        
        public void Initialize(string headerText)
        {
            _text.text = headerText;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    /// <summary>
    /// A struct that represents button appearance. It contains button sprite, text and text bottom offset.
    /// </summary>
    [Serializable]
    public struct ButtonAppearance
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _text;
        [SerializeField] private int _bottomOffset;

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public string GetText()
        {
            return _text;
        }

        public int GetOffset()
        {
            return _bottomOffset;
        }
    }
}

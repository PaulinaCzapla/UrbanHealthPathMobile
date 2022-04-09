using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    /// <summary>
    /// It adjusts the size of the RawImage with map. 
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(RawImage))]
    public class MapImageScaler : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private RectTransform _parentRectTransform;
        private RawImage _image;
        private float _width = 1;
        private float _height = 1;
        private float _x;
        private float _y;
        private float _rectWidth;
        private float _rectHeight;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentRectTransform = _rectTransform.parent.GetComponent<RectTransform>();
            _image = GetComponent<RawImage>();
        }

        private void Start()
        {
            StartCoroutine(SetRect());
        }

        IEnumerator SetRect()
        {
            yield return new WaitForEndOfFrame();
            _rectTransform.sizeDelta = _parentRectTransform.sizeDelta;
            _rectHeight = _rectTransform.rect.height;
            _rectWidth = _rectTransform.rect.width;
            
            CalculateImageRect();
            _image.uvRect = new Rect(new Vector2(_x, _y), new Vector2(_width, _height));
        }
        private void CalculateImageOffset(float ratio, float paramFirst, float paramSecond)
        {
            float diff = paramFirst - paramSecond;
            float result = diff / (paramFirst * 2);

            if (ratio < 1)
            {
                _x = result;
                _y = 0;
            }
            else
            {
                _x = 0;
                _y = result;
            }
        }

        private void CalculateImageRect()
        {
            float ratio = _rectWidth / _rectHeight;

            if (!Mathf.Approximately(ratio, 0))
            {
                float paramFirst, paramSecond;
                if (ratio < 1)
                {
                    _width = ratio;
                    _height = 1;
                    paramFirst = _rectHeight;
                    paramSecond = _rectWidth;
                }
                else
                {
                    _width = 1;
                    _height = _rectHeight / _rectWidth;
                    paramFirst = _rectWidth;
                    paramSecond = _rectHeight;
                }

                CalculateImageOffset(ratio, paramFirst, paramSecond);
            }
        }
    }
}
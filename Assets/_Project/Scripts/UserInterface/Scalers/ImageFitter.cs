using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    /// <summary>
    /// It adjusts a size of the image to fit given imageArea. It keeps the aspect ratio of the image.
    /// </summary>
    public class ImageFitter : MonoBehaviour
    {
        [FormerlySerializedAs("imageArea")] [SerializeField] private RectTransform _imageArea;
        [FormerlySerializedAs("imageRect")] [SerializeField] private RectTransform _imageRect;
        [FormerlySerializedAs("image")] [SerializeField] private RawImage _image;
        [SerializeField] private float _borderSize = 0;
        
        private void Start()
        {
            if (_image.texture)
            {
                InitializeImage(_image.texture);
            }
        }
        public void InitializeImage(Texture texture)
        {
            StartCoroutine(SetTextureSize(texture));
        }

        private IEnumerator SetTextureSize(Texture texture)
        {
            yield return new WaitForEndOfFrame();
            
            Vector2 size = TextureScaler.GetScaledTexture(_imageArea, texture, _borderSize);
            _image.texture = texture;
            _imageRect.sizeDelta = new Vector2(size.x, size.y);
        }
    }
}
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    /// <summary>
    /// It scales a given texture keeping the aspect ratio of the image. 
    /// </summary>
    public class TextureScaler
    {
        public static Vector2 GetScaledTexture(RectTransform maxSize, Texture texture, float borderSize)
        {
            float maxWidth = maxSize.sizeDelta.x;
            float maxHeight = maxSize.sizeDelta.y;

            float textureWidth = texture.width;
            float texturHeight = texture.height;

            float ratio = textureWidth / texturHeight;

            float targetWidth = textureWidth, targetHeight = texturHeight;

            if (textureWidth > texturHeight)
            {
                targetWidth = maxWidth;
                targetHeight = targetWidth / ratio;

                if (maxHeight < targetHeight)
                {
                    targetHeight = maxHeight;
                    targetWidth = ratio * targetHeight;
                }
            }
            else
            {
                targetHeight = maxHeight;
                targetWidth = ratio * targetHeight;

                if (maxWidth < targetWidth)
                {
                    targetWidth = maxWidth;
                    targetHeight = targetWidth / ratio;
                }
            }

            return new Vector2(targetWidth - borderSize, targetHeight - borderSize);
        }
    }
}
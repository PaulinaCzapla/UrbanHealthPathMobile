using System;
using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map.MapMovement
{
    /// <summary>
    /// A class that receives touch (swipe) input and invokes map swipe event.
    /// </summary>
    public class MapSwipe : MonoBehaviour
    {
        [SerializeField] private Vector3EventChannelSO _onMapSwiped;
        [SerializeField] private RectTransform _mapArea;

        private Vector3 _startPosition;
        private Vector3 _prevPosition;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (RectTransformUtility.RectangleContainsScreenPoint(
                    _mapArea, touch.position, null
                ))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _startPosition = new Vector3(touch.position.x, 0, touch.position.y);
                        _prevPosition = _startPosition;
                    }
                    else if (touch.phase == TouchPhase.Moved && Vector2.Distance(_startPosition, touch.position) >= 10)
                    {
                        Vector3 move = new Vector3(_prevPosition.x - touch.position.x,
                            0, _prevPosition.z - touch.position.y);

                        _prevPosition= new Vector3(touch.position.x, 0, touch.position.y);
                        _onMapSwiped.RaiseEvent(move);
                    }
                    else
                    {
                        _prevPosition = new Vector3(touch.position.x, 0, touch.position.y);
                    }
                }
                else
                {
                    _startPosition =  new Vector3(touch.position.x, 0, touch.position.y);
                    _prevPosition = _startPosition;
                }
            }
        }
    }
}
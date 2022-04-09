using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    /// <summary>
    /// A class that manages popups. It contains a table with all available popups and stores information
    /// about currently opened popup. It can open and close popups.
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        public event Action<PopupType> PopupOpened;
        public event Action<PopupType> PopupClosed;
        
        public PopupType CurrentPopupType { get; private set; }
        
        [SerializeField] private Popup[] popupsWithTypes;

        private Dictionary<PopupType, GameObject> _popups;

        private GameObject _currentPopup;
        
        public void Initialize()
        {
            _currentPopup = null;
            _popups = new Dictionary<PopupType, GameObject>();
            
            foreach (var popup in popupsWithTypes)
            {
                _popups.Add(popup.GetPopupType(), popup.GetPopupObject());
            }
        }

        /// <summary>
        /// A method that instantiates and initializes given popup. 
        /// </summary>
        /// <param name="popupType"> A type of popup that should be opened. </param>
        /// <param name="initializationParameters"> Optional parameter with popup's initialization params. </param>
        public GameObject OpenPopup(PopupType popupType, IPopupInitializationParameters initializationParameters = null)
        {
            CurrentPopupType = popupType;
            _currentPopup.Destroy();
            if (popupType != PopupType.None)
            {
                _currentPopup = Instantiate(_popups[popupType]);

                if (initializationParameters != null)
                {
                    IInitializablePopup initializablePopup = _currentPopup.GetComponent<IInitializablePopup>();
                    initializablePopup?.Initialize(initializationParameters);
                }
                
                PopupOpened?.Invoke(popupType);
                return _currentPopup;
            }

            return null;
        }

        public void CloseCurrentPopup()
        {
            if (CurrentPopupType != PopupType.None)
            {
                PopupType oldType = CurrentPopupType;
                CurrentPopupType = PopupType.None;
                _currentPopup.Destroy();
                PopupClosed?.Invoke(oldType);
            }
        }
    }
}
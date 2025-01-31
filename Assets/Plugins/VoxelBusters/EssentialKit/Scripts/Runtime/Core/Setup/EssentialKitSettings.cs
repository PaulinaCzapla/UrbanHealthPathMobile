﻿using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit
{
    public class EssentialKitSettings : ScriptableObject
    {
        #region Static fields

        private     static      EssentialKitSettings    s_sharedInstance                = null;

        #endregion

        #region Fields

        [SerializeField]
        private     ApplicationSettings                 m_applicationSettings           = new ApplicationSettings();

        [SerializeField]
        private     AddressBookUnitySettings            m_addressBookSettings           = new AddressBookUnitySettings();

        [SerializeField]
        private     NativeUIUnitySettings               m_nativeUISettings              = new NativeUIUnitySettings();

        [SerializeField]
        private     SharingServicesUnitySettings        m_sharingServicesSettings       = new SharingServicesUnitySettings();

        
        [SerializeField]
        private     NetworkServicesUnitySettings        m_networkServicesSettings       = new NetworkServicesUnitySettings();

        #endregion

        #region Static properties

        public static string PackageName { get { return "com.voxelbusters.essentialkit"; } }

        public static string DisplayName { get { return "Essential Kit - Free"; } }

        public static string Version { get { return "2.1.1"; } }

        public static string DefaultSettingsAssetName { get { return "EssentialKitSettings"; } }

        public static string DefaultSettingsAssetPath { get { return "Assets/Resources/" + DefaultSettingsAssetName + ".asset"; } }

        public static EssentialKitSettings Instance
        {
            get { return GetSharedInstanceInternal(); }
        }

        #endregion

        #region Properties

        public ApplicationSettings ApplicationSettings
        {
            get
            {
                return m_applicationSettings;
            }
            set
            {
                m_applicationSettings   = value;
            }
        }

        public AddressBookUnitySettings AddressBookSettings
        {
            get
            {
                return m_addressBookSettings;
            }
            set
            {
                m_addressBookSettings   = value;
            }
        }

        public NativeUIUnitySettings NativeUISettings
        {
            get
            {
                return m_nativeUISettings;
            }
            set
            {
                m_nativeUISettings    = value;
            }
        }

        public SharingServicesUnitySettings SharingServicesSettings
        {
            get
            {
                return m_sharingServicesSettings;
            }
            set
            {
                m_sharingServicesSettings   = value;
            }
        }


        public NetworkServicesUnitySettings NetworkServicesSettings
        {
            get
            {
                return m_networkServicesSettings;
            }
            set
            {
                m_networkServicesSettings   = value;
            }
        }

        #endregion

        #region Static methods

        public static void SetSettings(EssentialKitSettings settings)
        {
            Assert.IsArgNotNull(settings, nameof(settings));

            // set properties
            s_sharedInstance    = settings;
        }

        private static EssentialKitSettings GetSharedInstanceInternal(bool throwError = true)
        {
            if (null == s_sharedInstance)
            {
                // check whether we are accessing in edit or play mode
                var     assetPath   = DefaultSettingsAssetName;
                var     settings    = Resources.Load<EssentialKitSettings>(assetPath);
                if (throwError && (null == settings))
                {
                    throw Diagnostics.PluginNotConfiguredException();
                }

                // store reference
                s_sharedInstance = settings;
            }

            return s_sharedInstance;
        }

        #endregion

        #region Feature methods

        public string[] GetAvailableFeatureNames()
        {
            return new string[]
            {
                NativeFeatureType.kAddressBook,
                NativeFeatureType.kBillingServices,
                NativeFeatureType.kCloudServices,
                NativeFeatureType.kGameServices,
                NativeFeatureType.kMediaServices,
                NativeFeatureType.kNativeUI,
                NativeFeatureType.kNetworkServices,
                NativeFeatureType.kNotificationServices,
                NativeFeatureType.KSharingServices,
                NativeFeatureType.kWebView,
                NativeFeatureType.kDeepLinkServices
            };
        }

        public string[] GetUsedFeatureNames()
        {
            var     usedFeatures    = new List<string>();
            if (m_addressBookSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kAddressBook);
            }
            if (m_nativeUISettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kNativeUI);
            }
            if (m_networkServicesSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kNetworkServices);
            }
            if (m_sharingServicesSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.KSharingServices);
            }
            if ((usedFeatures.Count > 0) || (m_applicationSettings.RateMyAppSettings.IsEnabled))
            {
                usedFeatures.Add(NativeFeatureType.kExtras);
                usedFeatures.Add(NativeFeatureType.kNativeUI);//Required for showing confirmation dialog
            }

            return usedFeatures.ToArray();
        }

        public bool IsFeatureUsed(string name)
        {
            return Array.Exists(GetUsedFeatureNames(), (item) => string.Equals(item, name));
        }

        #endregion
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit.Editor
{
    public class UpgradeUtility
    {
        #region Static methods

        public static EssentialKitSettings ImportSettings()
        {
            // open file
            string  filePath    = EditorUtility.OpenFilePanel("Open NPSettings", "", "json");
            if (string.IsNullOrEmpty(filePath))
            {
                DebugLogger.LogError("Failed to locate json file");
                return null;
            }
            string  jsonStr     = IOServices.ReadFile(filePath);
            var     jsonDict    = (IDictionary)ExternalServiceProvider.JsonServiceProvider.FromJson(jsonStr);   

            // load properties
            var     applicationSettings     = ImportApplicationSettingsFromJson(jsonDict);
            var     addressBookSettings     = ImportAddressBookSettingsFromJson(jsonDict);
            var     networkSettings         = ImportNetworkServicesSettingsFromJson(jsonDict);
            var     sharingServicesSettings = ImportSharingServicesSettingsFromJson(jsonDict);
            
            // set properties
            var     essentialKitSettings    = EssentialKitSettingsEditorUtility.DefaultSettings;
            essentialKitSettings.ApplicationSettings                = applicationSettings;
            essentialKitSettings.AddressBookSettings                = addressBookSettings;
            essentialKitSettings.NetworkServicesSettings            = networkSettings;
            essentialKitSettings.SharingServicesSettings            = sharingServicesSettings;

            return essentialKitSettings;
        }

        #endregion

        #region Import methods

        private static ApplicationSettings ImportApplicationSettingsFromJson(IDictionary jsonDict)
        {
            // read properties
            string  iosId               = ReadJsonValueAtPath<string>(jsonDict, "m_applicationSettings.m_iOS.m_storeIdentifier");
            string  androidId           = ReadJsonValueAtPath<string>(jsonDict, "m_applicationSettings.m_android.m_storeIdentifier");
            var     rateMyAppSettings   = ImportRateMyAppSettingsFromJson(jsonDict);
            
            // create object
            return new ApplicationSettings(
                appStoreIds: new NativePlatformConstantSet(ios: iosId, tvos: iosId, android: androidId),
                rateMyAppSettings: rateMyAppSettings);
        }

        private static AddressBookUnitySettings ImportAddressBookSettingsFromJson(IDictionary jsonDict)
        {
            // read properties
            bool    isEnabled   = GetUsesFeatureValueFromJson(jsonDict, "m_usesAddressBook");

            // create object
            return new AddressBookUnitySettings(isEnabled);
        }

        private static NetworkServicesUnitySettings ImportNetworkServicesSettingsFromJson(IDictionary jsonDict)
        {
            // read properties
            bool    isEnabled       = GetUsesFeatureValueFromJson(jsonDict,         "m_usesNetworkConnectivity");

            var     settingsDict    = ReadJsonValueAtPath<IDictionary>(jsonDict,    "m_networkConnectivitySettings");
            string  ipv4Address     = ReadJsonValueAtPath<string>(settingsDict,     "m_hostAddressIPV4");
            string  ipv6Address     = ReadJsonValueAtPath<string>(settingsDict,     "m_hostAddressIPV6");
            float   timeOutPeriod   = ReadJsonValueAtPath<float>(settingsDict,      "m_timeOutPeriod");
            int     maxRetry        = ReadJsonValueAtPath<int>(settingsDict,        "m_maxRetryCount");
            float   timeGap         = ReadJsonValueAtPath<float>(settingsDict,      "m_timeGapBetweenPolling");
            int     port            = ReadJsonValueAtPath<int>(settingsDict,        "m_android.m_port");

            // create object
            var     hostAddress     = new NetworkServicesUnitySettings.Address(ipv4Address, ipv6Address);
            var     pingSettings    = new NetworkServicesUnitySettings.PingTestSettings(maxRetry, timeGap, timeOutPeriod, port);
            return new NetworkServicesUnitySettings(isEnabled, hostAddress, true, pingSettings);
        }

        private static RateMyAppSettings ImportRateMyAppSettingsFromJson(IDictionary jsonDict)
        {
            // read properties
            var     rateMyAppDict                   = ReadJsonValueAtPath<IDictionary>(jsonDict,        "m_utilitySettings.m_rateMyApp");
            bool    isEnabled                       = ReadJsonValueAtPath<bool>(rateMyAppDict,          "m_isEnabled");
            string  title                           = ReadJsonValueAtPath<string>(rateMyAppDict,        "m_title");
            string  message                         = ReadJsonValueAtPath<string>(rateMyAppDict,        "m_message");
            int     showFirstPromptAfterHours       = ReadJsonValueAtPath<int>(rateMyAppDict,           "m_showFirstPromptAfterHours");
            int     successivePromptAfterHours      = ReadJsonValueAtPath<int>(rateMyAppDict,           "m_successivePromptAfterHours");
            int     successivePromptAfterLaunches   = ReadJsonValueAtPath<int>(rateMyAppDict,           "m_successivePromptAfterLaunches");
            string  okLabel                         = ReadJsonValueAtPath<string>(rateMyAppDict,        "m_rateItButtonText");
            string  cancelLabel                     = ReadJsonValueAtPath<string>(rateMyAppDict,        "m_remindMeLaterButtonText");
            string  remindLaterLabel                = ReadJsonValueAtPath<string>(rateMyAppDict,        "m_dontAskButtonText");

            // create object    
            var     dialogSettings                  = new RateMyAppConfirmationDialogSettings(true, title, message, okLabel, cancelLabel, remindLaterLabel, true);
            var     defaultValidatorSettings        = new RateMyAppDefaultControllerSettings(new RateMyAppDefaultControllerSettings.PromptConstraints(showFirstPromptAfterHours, 0), new RateMyAppDefaultControllerSettings.PromptConstraints(successivePromptAfterHours, successivePromptAfterLaunches));
            return new RateMyAppSettings(isEnabled, dialogSettings, defaultValidatorSettings);
        }

        private static SharingServicesUnitySettings ImportSharingServicesSettingsFromJson(IDictionary jsonDict)
        {
            // read properties
            bool    isEnabled       = GetUsesFeatureValueFromJson(jsonDict, "m_usesSharing");

            // create object
            return new SharingServicesUnitySettings(isEnabled);
        }

        #endregion

        #region Read methods

        private static T ReadJsonValueAtPath<T>(IDictionary jsonDict, string keyPath)
        {
            var     pathComponents  = keyPath.Split('.');
            var     dictionary      = jsonDict;

            // traverse through path
            int     iter            = 0;
            while (iter < (pathComponents.Length - 1))
            {
                string  currentKey  = pathComponents[iter++];
                dictionary          = (IDictionary)dictionary[currentKey];
            }

            // read last key value
            string  lastKey         = pathComponents[pathComponents.Length - 1];
            object  value           = dictionary[lastKey];

            // typecast return value
            if (value == null)
            {
                return default(T);
            }

            var     targetType      = typeof(T);
            if (targetType.IsInstanceOfType(value))
            {
                return (T)value;
            }

#if !NETFX_CORE
            if (targetType.IsEnum)
#else
            if (targetType.GetTypeInfo().IsEnum)
#endif
            {
                return (T)Enum.ToObject(targetType, value);
            }
            else
            {
                return (T)Convert.ChangeType(value, targetType);
            }
        }

        private static bool GetUsesFeatureValueFromJson(IDictionary jsonDict, string flagName)
        {
            return ReadJsonValueAtPath<bool>(jsonDict, "m_applicationSettings.m_supportedFeatures." + flagName);
        }

        private static PlatformConstant[] ConvertToPlatformConstants(IList jsonArray)
        {
            var     constantList    = new List<PlatformConstant>();
            foreach (IDictionary idDict in jsonArray)
            {
                int     platform    = ReadJsonValueAtPath<int>(idDict,      "m_platform");
                string  value       = ReadJsonValueAtPath<string>(idDict,   "m_value");

                // add to list
                switch (platform)
                {
                    case 0:
                        constantList.Add(PlatformConstant.iOS(value));
                        break;

                    case 1:
                        constantList.Add(PlatformConstant.Android(value));
                        break;
                }
            }

            return constantList.ToArray();
        }

        #endregion
    }
}
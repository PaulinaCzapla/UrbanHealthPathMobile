using System.Collections.Generic;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.Navigation;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.Player;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Holder of the Map prefab which is used to generate map in the game.
    /// </summary>
    public class MapHolder : MonoBehaviour
    {
        [SerializeField] private StationFactory _stationFactory;
        [SerializeField] private NavigationPointProvider _navigationPointProvider;
        [SerializeField] private PlayerLocationTransformer _playerLocationTransformer;
        [SerializeField] private TransformHeadingRotator _transformHeadingRotator;
        [SerializeField] private DirectionsFactory _directionsFactory;
        [SerializeField] private AbstractMap _map;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private RenderTexture _renderTexture;

        private bool _isInitialized;

        public void Initialize(string mapUrl, ILocationUpdater locationUpdater, List<Coordinates> stationsCoordinates)
        {
            SetMapSource(mapUrl);
            _transformHeadingRotator.Initialize(locationUpdater);
            new DelayedMapInitializer(_map, locationUpdater);
            _playerLocationTransformer.Initialize(_map, locationUpdater);
            _navigationPointProvider.Initialize(_map, stationsCoordinates[0]);
            _directionsFactory.Initialize(_map);
            _stationFactory.Initialize(_map, locationUpdater, stationsCoordinates);
            _camera.targetTexture = _renderTexture;
            _camera.enabled = true;
            _isInitialized = true;
        }

        public void EnableNavigation()
        {
            _navigationPointProvider.PutOnMap();
            _directionsFactory.CallQuery();
        }

        public void DisableNavigation()
        {
            _directionsFactory.DestroyNavigationLine();
        }

        public void OnDestroy()
        {
            if (_isInitialized)
            {
                _camera.enabled = false;
                _camera.targetTexture = null;
            }
        }

        public void MoveHalo()
        {
            _stationFactory.MoveStationHalo();
        }

        private void SetMapSource(string mapUrl)
        {
            _map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            _map.ImageLayer.SetLayerSource(mapUrl);
        }
    }
}
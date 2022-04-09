using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Statistics;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using PolSl.UrbanHealthPath.Utils.PermissionManagement;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for managing current path state.
    /// </summary>
    public class PathController : BaseController
    {
        public event Action<UrbanPath> PathStarted;
        public event Action<UrbanPath> PathCancelled;
        public event Action<UrbanPath> PathCompleted;

        private readonly IPathProgressManager _pathProgressManager;
        private readonly Action _returnToMainMenu;
        private readonly ILocationProviderFactory _locationProviderFactory;
        private readonly MapHolder _mapHolderPrefab;
        private readonly CoroutineManager _coroutineManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IFinishedPathStatisticsProvider _finishedPathStatisticsProvider;
        private readonly IPathStatisticsLoggerFactory _pathStatisticsLoggerFactory;

        private IPathStatisticsLogger _pathStatisticsLogger;
        private UrbanPath _selectedPath;
        private MapHolder _mapHolder;

        private IEnumerator _locationUpdateCoroutine;

        private bool _isNextStationButtonActive;
        private bool _wasStationFinishedSinceLastLocationUpdate = true;

        public UrbanPath CurrentPath => _selectedPath;

        public PathController(ViewManager viewManager, PopupManager popupManager,
            IPathProgressManager pathProgressManager,
            Action returnToMainMenu, ILocationProviderFactory locationProviderFactory,
            MapHolder mapHolderPrefab, CoroutineManager coroutineManager, IPermissionManager permissionManager,
            IFinishedPathStatisticsProvider finishedPathStatisticsProvider,
            IPathStatisticsLoggerFactory pathStatisticsLoggerFactory) : base(viewManager, popupManager)
        {
            _pathProgressManager = pathProgressManager;
            _returnToMainMenu = returnToMainMenu;
            _locationProviderFactory = locationProviderFactory;
            _mapHolderPrefab = mapHolderPrefab;
            _coroutineManager = coroutineManager;
            _permissionManager = permissionManager;
            _finishedPathStatisticsProvider = finishedPathStatisticsProvider;
            _pathStatisticsLoggerFactory = pathStatisticsLoggerFactory;
        }

        public void ShowPathSelectionView(IList<UrbanPath> availablePaths, bool areDemoPaths)
        {
            IViewInitializationParameters initParams =
                new PathChoiceViewInitializationParameters(
                    areDemoPaths
                        ? BuildButtonsForAvailablePaths(availablePaths,
                            path => ShowPathPresentationView(path, StartNewDemoPath))
                        : BuildButtonsForAvailablePaths(availablePaths,
                            path => ShowPathPresentationView(path, StartNewPath)),
                    () => _returnToMainMenu.Invoke());

            ViewManager.OpenView(ViewType.PathChoice, initParams);
        }

        public void ShowPathPresentationView(UrbanPath path, Action<UrbanPath> startPath)
        {
            int stationsCount = path.GetWaypointsOfType<Station>().Count;
            Texture mapTexture = new TextureFileAccessor(path.PreviewImage).GetMedia();

            IViewInitializationParameters initParams = new PathPresentationViewInitializationParameters(
                () => _returnToMainMenu.Invoke(), ReturnToPreviousView, () => startPath.Invoke(path),
                path.DisplayedName, stationsCount, path.ApproximateDistanceInMeters, mapTexture);

            ViewManager.OpenView(ViewType.PathPresentation, initParams);
        }

        public void ShowPathView(Action nextStationButtonPressed, Action nextStationButtonCanceled,
            Action helpButtonPressed)
        {
            if (_selectedPath is null)
            {
                return;
            }

            ViewManager.OpenView(ViewType.Path, new PathViewInitializationParameters(
                () => ShowConfirmation("Czy na pewno chcesz zakończyć?", CancelPath),
                BuildNextStationButton(nextStationButtonPressed, nextStationButtonCanceled),
                () => helpButtonPressed.Invoke(),
                () => _returnToMainMenu.Invoke(), _selectedPath.DisplayedName
            ));
        }

        private UnityAction BuildNextStationButton(Action clickedInactive, Action clickedActive)
        {
            return () =>
            {
                if (_isNextStationButtonActive)
                {
                    clickedActive.Invoke();
                }
                else
                {
                    clickedInactive.Invoke();
                }

                _isNextStationButtonActive = !_isNextStationButtonActive;
            };
        }

        public void CancelPath()
        {
            _pathProgressManager.CancelPath();
            OnPathCancelled(_selectedPath);
        }

        private void SelectPath(UrbanPath path)
        {
            _selectedPath = path;
        }

        private List<ListElement> BuildButtonsForAvailablePaths(IList<UrbanPath> availablePaths,
            Action<UrbanPath> presentPath)
        {
            return availablePaths.Select(path => new ListElement(path.DisplayedName, 
                new TextureFileAccessor(path.Icon).GetMedia(), Color.white, "",
                () => presentPath.Invoke(path))).ToList();
        }

        private void InitializeMapHolderForDemoPath(string mapUrl, List<Coordinates> coordinatesList)
        {
            DestroyMap();

            ILocationProvider locationProvider = _locationProviderFactory.CreateProvider(coordinatesList);
            ILocationUpdater locationUpdater =
                new LimitedLocationUpdaterDecorator(locationProvider, CheckIfStationFinishedSinceLastLocationUpdate);
            _locationUpdateCoroutine = locationUpdater.UpdateLocation();

            _mapHolder = UnityEngine.Object.Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(mapUrl, locationUpdater, coordinatesList);
            
            _coroutineManager.BeginCoroutine(_locationUpdateCoroutine);
        }

        private void InitializeMapHolder(string mapUrl, List<Coordinates> coordinatesList)
        {
            DestroyMap();

            ILocationProvider locationProvider = _locationProviderFactory.CreateProvider();
            ILocationUpdater locationUpdater = new LocationUpdater(locationProvider);
            _locationUpdateCoroutine = locationUpdater.UpdateLocation();

            _mapHolder = UnityEngine.Object.Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(mapUrl, locationUpdater, coordinatesList);

            _coroutineManager.BeginCoroutine(_locationUpdateCoroutine);
            _mapHolder.EnableNavigation();
        }

        private void DestroyMap()
        {
            if (_mapHolder is null)
            {
                return;
            }

            if (_locationUpdateCoroutine != null)
            {
                _coroutineManager.EndCoroutine(_locationUpdateCoroutine);
                _locationUpdateCoroutine = null;
                _wasStationFinishedSinceLastLocationUpdate = true;
            }

            UnityEngine.Object.Destroy(_mapHolder.gameObject);
            _mapHolder = null;
        }

        private void StartNewPath(UrbanPath urbanPath)
        {
            _permissionManager.RequirePermission(Permission.Location, result =>
            {
                if (result == RequestResult.Granted)
                {
                    SelectPath(urbanPath);
                    _pathStatisticsLogger = _pathStatisticsLoggerFactory.GetLogger();
                    _pathProgressManager.CheckpointReached += CheckpointReachedHandler;
                    _pathProgressManager.StartNewPath();
                    InitializeMapHolder(urbanPath.MapUrl, urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
                    OnPathStarted(urbanPath);
                }
            });
        }

        private void StartNewDemoPath(UrbanPath urbanPath)
        {
            SelectPath(urbanPath);
            _pathStatisticsLogger = _pathStatisticsLoggerFactory.GetLogger(true);
            _pathProgressManager.CheckpointReached += CheckpointReachedHandler;
            _pathProgressManager.StartNewPath();
            InitializeMapHolderForDemoPath(urbanPath.MapUrl, urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
            OnPathStarted(urbanPath);
        }

        private void CheckpointReachedHandler(object sender, CheckpointReachedEventArgs e)
        {
            if (e.Checkpoint.WaypointId == _selectedPath.Waypoints[_selectedPath.Waypoints.Count - 1].Value.WaypointId)
            {
                CompletePath();
                return;
            }

            if (_selectedPath.Waypoints.FirstOrDefault(x => x.Value.WaypointId == e.Checkpoint.WaypointId)
                ?.Value is Station station)
            {
                OnStationComplete(station);
            }

            ReturnToPreviousView();
        }

        private void CompletePath()
        {
            _pathProgressManager.CompletePath();
            OnPathComplete(_selectedPath);
        }

        private void FinishPath()
        {
            _pathProgressManager.CheckpointReached -= CheckpointReachedHandler;
            _selectedPath = null;
        }

        public void ShowCompletedPathSummary(UrbanPath completedPath, Action shareButtonClicked)
        {
            ShowPathSummary(completedPath, shareButtonClicked, completedPath.GetWaypointsOfType<Station>().Count,
                completedPath.ApproximateDistanceInMeters);
        }

        public void ShowCancelledPathSummary(UrbanPath cancelledPath, Action shareButtonClicked)
        {
            IList<Station> stations = cancelledPath.GetWaypointsOfType<Station>();

            int visitedPointsCount;

            if (_pathProgressManager.LastCheckpoint is null)
            {
                visitedPointsCount = 0;
            }
            else
            {
                Station lastStation =
                    stations.First(x => x.WaypointId == _pathProgressManager.LastCheckpoint.WaypointId);
                visitedPointsCount = stations.IndexOf(lastStation) + 1;
            }

            int distance = cancelledPath.ApproximateDistanceInMeters * visitedPointsCount / stations.Count;

            ShowPathSummary(cancelledPath, shareButtonClicked, visitedPointsCount, distance);
        }

        private void ShowPathSummary(UrbanPath path, Action shareButtonClicked, int visitedPointCount, int distance)
        {
            ViewManager.OpenView(ViewType.PathSummary,
                new PathSummaryViewInitializationParameters(() => _returnToMainMenu.Invoke(),
                    () => shareButtonClicked.Invoke(), visitedPointCount, distance));
        }

        private void OnPathStarted(UrbanPath path)
        {
            PathStarted?.Invoke(path);
        }

        private void OnPathCancelled(UrbanPath path)
        {
            _pathStatisticsLogger.LogCancelledPathStatistics(
                _finishedPathStatisticsProvider.GetFinishedPathStatistics(_pathProgressManager, _selectedPath));
            FinishPath();
            PathCancelled?.Invoke(path);
        }

        private void OnPathComplete(UrbanPath path)
        {
            _pathStatisticsLogger.LogCompletedPathStatistics(
                _finishedPathStatisticsProvider.GetFinishedPathStatistics(_pathProgressManager, _selectedPath));
            FinishPath();
            PathCompleted?.Invoke(path);
        }

        private void OnStationComplete(Station station)
        {
            _wasStationFinishedSinceLastLocationUpdate = true;

            _mapHolder.MoveHalo();
            _mapHolder.DisableNavigation();
        }

        protected override void ViewOpenedHandler(ViewType type)
        {
            base.ViewOpenedHandler(type);

            if (type == ViewType.Path)
            {
                _isNextStationButtonActive = false;
            }
        }

        private bool CheckIfStationFinishedSinceLastLocationUpdate()
        {
            if (_wasStationFinishedSinceLastLocationUpdate)
            {
                _wasStationFinishedSinceLastLocationUpdate = false;
                return true;
            }

            return false;
        }
    }
}
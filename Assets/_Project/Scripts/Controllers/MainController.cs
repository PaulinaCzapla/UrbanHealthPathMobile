using System;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Statistics;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using PolSl.UrbanHealthPath.Utils.PermissionManagement;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Main application controller that creates and sets up the rest of them.
    /// </summary>
    public class MainController : BaseController
    {
        private readonly IPathProgressManager _pathProgressManager;
        private readonly IApplicationData _applicationData;
        private readonly MapHolder _mapHolderPrefab;
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;
        private readonly IPermissionManager _permissionManager;
        private readonly IPathStatisticsLoggerFactory _pathStatisticsLoggerFactory;
        private readonly IFinishedPathStatisticsProvider _finishedPathStatisticsProvider;

        private LoginController _loginController;
        private MenuController _menuController;
        private SettingsController _settingsController;
        private HelpController _helpController;
        private PathController _pathController;
        private StationController _stationController;
        private ExerciseController _exerciseController;
        private ShareController _shareController;
        private ProfileController _profileController;
        private TestController _testController;

        public MainController(ViewManager viewManager, PopupManager popupManager,
            IPathProgressManager pathProgressManager, IApplicationData applicationData, MapHolder mapHolderPrefab,
            CoroutineManager coroutineManager, Settings settings, IPermissionManager permissionManager,
            IPathStatisticsLoggerFactory pathStatisticsLoggerFactory,
            IFinishedPathStatisticsProvider finishedPathStatisticsProvider) : base(viewManager,
            popupManager)
        {
            _pathProgressManager = pathProgressManager;
            _applicationData = applicationData;
            _mapHolderPrefab = mapHolderPrefab;
            _coroutineManager = coroutineManager;
            _settings = settings;
            _permissionManager = permissionManager;
            _pathStatisticsLoggerFactory = pathStatisticsLoggerFactory;
            _finishedPathStatisticsProvider = finishedPathStatisticsProvider;
        }

        public void Run()
        {
            CreateControllers();
            SubscribeToMenuEvents();
            SubscribeToPathEvents();

            _loginController.ShowLoginScreenOnFirstRun(ReturnToMenu);
        }

        private void SubscribeToMenuEvents()
        {
            _menuController.SettingsButtonPressed += () => _settingsController.ShowSettings(
                ReturnToMenu, () => { }
            );

            _menuController.HelpButtonPressed += _helpController.ShowHelp;

            _menuController.ProfileButtonPressed += _profileController.ShowProfile;

            _menuController.TopMenuButtonPressed += HandleTopButtonClick;

            _menuController.BottomMenuButtonPressed += HandleBottomButtonClick;
        }

        private void SubscribeToPathEvents()
        {
            _pathController.PathStarted += BuildPathView;
            _pathController.PathCancelled += path =>
                _pathController.ShowCancelledPathSummary(path, _shareController.ShareDefaultWhatsappMessage);
            _pathController.PathCompleted += path =>
                _pathController.ShowCompletedPathSummary(path, _shareController.ShareDefaultWhatsappMessage);
        }

        private void BuildPathView(UrbanPath path)
        {
            _pathController.ShowPathView(() =>
            {
                Station nextStation = GetNextStation(path);
                _stationController.ShowNextStationConfirmation(nextStation,
                    () =>
                    {
                        _stationController.ShowStation(nextStation, _exerciseController.ShowPopupForExercise,
                            exercise => PopupManager.CloseCurrentPopup(),
                            station =>
                            {
                                _pathProgressManager.AddCheckpoint(
                                    new PathProgressCheckpoint(nextStation.WaypointId, DateTime.Now));
                            });
                    });
            }, () => { PopupManager.CloseCurrentPopup(); }, _helpController.ShowHelp);
        }

        private void CreateControllers()
        {
            _loginController = new LoginController(ViewManager, PopupManager);
            _menuController = new MenuController(ViewManager, PopupManager);
            _settingsController = new SettingsController(ViewManager, PopupManager, _settings);
            _helpController = new HelpController(ViewManager, PopupManager);
            _pathController = new PathController(ViewManager, PopupManager, _pathProgressManager, ReturnToMenu,
                new LocationProviderFactory(), _mapHolderPrefab, _coroutineManager,
                _permissionManager, _finishedPathStatisticsProvider, _pathStatisticsLoggerFactory);
            _stationController = new StationController(ViewManager, PopupManager, _coroutineManager, _settings);
            _exerciseController = new ExerciseController(ViewManager, PopupManager, _coroutineManager);
            _profileController = new ProfileController(ViewManager, PopupManager,
                () => _testController.ShowTestIntroduction(_applicationData.Tests[0]));
            _testController = new TestController(ViewManager, PopupManager, _coroutineManager, ReturnToMenu,
                _profileController.ShowProfile, _exerciseController.ShowPopupForExercise,
                exercise => PopupManager.CloseCurrentPopup());
            _shareController = new ShareController();
        }

        private void ReturnToMenu()
        {
            _menuController.ShowMenu(_pathProgressManager.IsPathInProgress);
        }

        private void HandleTopButtonClick()
        {
            if (_pathProgressManager.IsPathInProgress)
            {
                _pathController.CancelPath();
            }
            else
            {
                _pathController.ShowPathSelectionView(_applicationData.UrbanPaths, false);
            }
        }

        private void HandleBottomButtonClick()
        {
            if (_pathProgressManager.IsPathInProgress)
            {
                BuildPathView(_pathController.CurrentPath);
            }
            else
            {
                _pathController.ShowPathSelectionView(_applicationData.UrbanPaths, true);
            }
        }

        private Station GetNextStation(UrbanPath path)
        {
            IList<Waypoint> pathWaypoints = path.Waypoints.Select(waypoint => waypoint.Value).ToList();

            PathProgressCheckpoint lastCheckpoint = _pathProgressManager.LastCheckpoint;

            if (lastCheckpoint is null)
            {
                return (Station) pathWaypoints.FirstOrDefault(x => x is Station);
            }

            Waypoint lastWaypoint = pathWaypoints.Single(x => x.WaypointId == lastCheckpoint.WaypointId);
            int indexOfLastWaypoint = pathWaypoints.IndexOf(lastWaypoint);

            for (int i = indexOfLastWaypoint + 1; i < pathWaypoints.Count; i++)
            {
                if (pathWaypoints[i] is Station station)
                {
                    return station;
                }
            }

            return null;
        }
    }
}
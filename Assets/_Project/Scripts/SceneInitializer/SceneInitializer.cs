using System.IO;
using Newtonsoft.Json;
using PolSl.UrbanHealthPath.Controllers;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Statistics;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using PolSl.UrbanHealthPath.Utils.PermissionManagement;
using PolSl.UrbanHealthPath.Utils.PersistentValue;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    /// <summary>
    /// Main scene's entry point that creates all required objects and injects them.
    /// </summary>
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private string _pathDataDirectory;
        
        [FormerlySerializedAs("_coroutinesManager")] [SerializeField]
        private CoroutineManager _coroutineManager;

        [SerializeField] private MapHolder _mapHolderPrefab;

        [SerializeField] private GameObject _uiManager;

        [SerializeField] private AudioSource _audioSource;

        private ITextLogger _logger;
        private IApplicationData _applicationData;
        private IPathProgressManager _pathProgressManager;
        private IPersistentValue<bool> _isFirstRun;
        private ViewManager _viewManager;
        private PopupManager _popupManager;
        private IPermissionManager _permissionManager;

        private void Awake()
        {
            _logger = new UnityLogger();
            _permissionManager = new PermissionManager(new AndroidPermissionAdapter(), _coroutineManager);

            _isFirstRun = new BoolPrefsValue("is_first_run", true);

            _applicationData = LoadApplicationData();
            _pathProgressManager =
                new PathProgressManager(new JsonFilePathProgressPersistor(Application.dataPath + "/progress.json",
                    JsonSerializer.Create()));

            GameObject uiManager = Instantiate(_uiManager);

            _viewManager = uiManager.GetComponent<ViewManager>();
            _viewManager.Initialize();

            _popupManager = uiManager.GetComponent<PopupManager>();
            _popupManager.Initialize();

            Settings settings = new Settings();

            MainController mainController = new MainController(_viewManager, _popupManager, _pathProgressManager,
                _applicationData, _mapHolderPrefab, _coroutineManager, settings, _permissionManager,
                new PathStatisticsLoggerFactory(_logger,
                    Path.Combine(Application.persistentDataPath, "statistics.csv")),
                new FinishedPathStatisticsProvider());

            mainController.Run();
        }

        private IApplicationData LoadApplicationData()
        {
            ILoadersFactory loadersFactory = new JsonFilesLoadersFactory(_pathDataDirectory, new JsonAssetFileReader(),
                new JsonDataLoaderParsersFactory().Create());
            IApplicationDataLoader applicationDataLoader = new ApplicationDataLoader(loadersFactory);

            return applicationDataLoader.LoadData();
        }
    }
}
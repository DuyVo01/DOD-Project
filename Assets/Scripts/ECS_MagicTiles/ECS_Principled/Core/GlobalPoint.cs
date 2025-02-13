using EventChannel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GlobalPoint : MonoBehaviour
    {
        [Header("Game Settings")]
        public GeneralGameSetting generalGameSetting;

        public MusicNoteCreationSetting musicNoteCreationSettings;

        public ECS_MagicTile.Settings.ScoreEffectSettings scoreEffectSettings;

        public PerfectLineSetting perfectLineSetting;

        [Header("Event Channel")]
        public IntEventChannel entityIdChannel;
        public BoolEventChannel OnScoreHitChannel;
        public BoolEventChannel OnOrientationChangedChannel;

        [Header("UI references")]
        public TextMeshProUGUI scoreText;
        public Slider progressSlider;

        [Header("Object references")]
        public GameObject perfectLineObject;
        public Camera mainCamera;

        private World world;

        public World World
        {
            get => world;
        }

        public MusicNoteViewSyncTool musicNoteViewSyncTool { get; private set; }
        public PerfectLineSyncTool perfectLineSyncTool { get; private set; }
        public StartingNoteSyncTool startingNoteSyncTool { get; private set; }
        public GameScoreSyncTool gameScoreSyncTool { get; private set; }

        private void Awake()
        {
            // Initialize our ECS world
            world = new World();

            SystemRegistry.Initialize(world);
            InitializeSyncTools();
            RegisterSystems();
        }

        private void InitializeSyncTools()
        {
            musicNoteViewSyncTool = new MusicNoteViewSyncTool(this);
            perfectLineSyncTool = new PerfectLineSyncTool(this);
            startingNoteSyncTool = new StartingNoteSyncTool(this);
            gameScoreSyncTool = new GameScoreSyncTool(this);
        }

        private void RegisterSystems()
        {
            //Singleton Creation system
            SystemRegistry.AddSystem(new SingletonCreationSystem(this));

            //Creation System
            SystemRegistry.AddSystem(new PerfectLineSystem(this));
            SystemRegistry.AddSystem(
                new MusicNoteCreationSystem(musicNoteCreationSettings, generalGameSetting)
            );
            SystemRegistry.AddSystem(new StartingNoteSystem(this));

            //Handling Data system
            SystemRegistry.AddSystem(new MovingNoteSystem(this));
            SystemRegistry.AddSystem(new InputSystem());
            SystemRegistry.AddSystem(new InputCollisionSystem(this));
            SystemRegistry.AddSystem(new ScoringSystem(this));
            SystemRegistry.AddSystem(new ProgressSystem(this));

            //Syncer systems
            SystemRegistry.AddSystem(new ScoreUISyncer(this));
            SystemRegistry.AddSystem(new ProgressSyncer(this));

            //Game State system
            SystemRegistry.AddSystem(new GameStateSystem(this));
        }

        private void Update()
        {
            // Update all systems with current frame's delta time
            SystemRegistry.Update(Time.deltaTime);
        }
    }
}

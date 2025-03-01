using EventChannel;
using StateMachineChart;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GlobalPoint : MonoBehaviour
    {
        [Header("Game Settings")]
        public GeneralGameSetting generalGameSetting;

        public MusicNoteCreationSetting musicNoteCreationSettings;

        public PerfectLineSetting perfectLineSetting;
        public LaneLineSettings laneLineSettings;

        [Header("Event Channel")]
        public IntEventChannel OnGameStartChannel;
        public BoolEventChannel OnScoreHitChannel;
        public BoolEventChannel OnOrientationChangedChannel;
        public EmptyEventChannel OnSongStartChannel;

        [Header("UI references")]
        public Text scoreText;
        public Slider progressSlider;

        [Header("Object references")]
        public GameObject perfectLineObject;
        public Camera mainCamera;

        [Header("Mono Settings")]
        public GameIntroSystem gameIntroSystem;
        public InGameUIElementHandlerSystem inGameUIElementHandlerSystem;
        private World world;

        public World World
        {
            get => world;
        }

        public MusicNoteViewSyncTool musicNoteViewSyncTool { get; private set; }
        public PerfectLineSyncTool perfectLineSyncTool { get; private set; }
        public StartingNoteSyncTool startingNoteSyncTool { get; private set; }
        public GameScoreSyncTool gameScoreSyncTool { get; private set; }
        public ProgressSyncTool progressSyncTool { get; private set; }
        public LaneLineSyncTool laneLineSyncTool { get; private set; }

        private StateChart stateChart;

        private void Awake()
        {
            // Initialize our ECS world
            world = new World();

            generalGameSetting.CurrentGameState = EGameState.Intro;

            SystemRegistry.Initialize(world);
            InitializeSyncTools();
            RegisterSystems();
            SetupStateChart();
        }

        private void InitializeSyncTools()
        {
            musicNoteViewSyncTool = new MusicNoteViewSyncTool(this);
            perfectLineSyncTool = new PerfectLineSyncTool(this);
            startingNoteSyncTool = new StartingNoteSyncTool(this);
            gameScoreSyncTool = new GameScoreSyncTool(this);
            progressSyncTool = new ProgressSyncTool(this);
            laneLineSyncTool = new LaneLineSyncTool(this);
        }

        private void SetupStateChart()
        {
            var rootState = new CompositeState();

            var introState = new GameSystemState(World, new IGameSystem[] { gameIntroSystem });
            var preStartState = new GameSystemState(
                World,
                new IGameSystem[]
                {
                    new StartingNoteSystem(this),
                    new PerfectLineSystem(this),
                    new LaneLineSystem(this),
                    inGameUIElementHandlerSystem,
                }
            );
            var ingameState = new GameSystemState(
                World,
                new IGameSystem[]
                {
                    new MusicNoteCreationSystem(this),
                    new MovingNoteSystem(this),
                    new TraceNoteToTriggerSongSystem(this),
                    new InputSystem(),
                    new InputCollisionSystem(this),
                    new ScoringSystem(this),
                    new ProgressSystem(this),
                }
            );

            rootState.AddSubstate(introState);
            rootState.AddSubstate(ingameState);

            stateChart = new StateChart(rootState);

            stateChart.AddTransition(
                introState,
                preStartState,
                () => generalGameSetting.CurrentGameState == EGameState.IngamePrestart
            );

            stateChart.AddTransition(
                preStartState,
                ingameState,
                () => generalGameSetting.CurrentGameState == EGameState.IngamePlaying
            );
        }

        private void RegisterSystems()
        {
            //Singleton Creation system
            SystemRegistry.AddSystem(new SingletonCreationSystem(this));

            // //Creation System
            // SystemRegistry.AddSystem(new PerfectLineSystem(this));
            // SystemRegistry.AddSystem(new MusicNoteCreationSystem(this));
            // SystemRegistry.AddSystem(new StartingNoteSystem(this));

            // //Handling Data system
            // SystemRegistry.AddSystem(new MovingNoteSystem(this));
            // SystemRegistry.AddSystem(new TraceNoteToTriggerSongSystem(this));
            // SystemRegistry.AddSystem(new InputSystem());
            // SystemRegistry.AddSystem(new InputCollisionSystem(this));
            // SystemRegistry.AddSystem(new ScoringSystem(this));
            // SystemRegistry.AddSystem(new ProgressSystem(this));
            // SystemRegistry.AddSystem(new LaneLineSystem(this));

            // //Game State system
            SystemRegistry.AddSystem(new GameStateSystem(this));
        }

        private void Update()
        {
            // Update all systems with current frame's delta time
            SystemRegistry.Update(Time.deltaTime);
            stateChart.Update();
        }
    }
}

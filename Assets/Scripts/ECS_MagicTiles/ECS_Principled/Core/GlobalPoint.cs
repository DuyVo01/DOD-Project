using ECS_MagicTile.Components;
using ECS_MagicTile.Settings;
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

        public PerfectLineSettingSO perfectLineSettingSO;
        public ECS_MagicTile.Settings.ScoreEffectSettings scoreEffectSettings;

        [Header("Event Channel")]
        public IntEventChannel entityIdChannel;
        public BoolEventChannel scoreEffectChannel;

        [Header("UI references")]
        public TextMeshProUGUI scoreText;
        public Slider progressSlider;

        private World world;

        private void Awake()
        {
            // Initialize our ECS world
            world = new World();

            SystemRegistry.Initialize(world);
            RegisterSystems();
        }

        private void RegisterSystems()
        {
            //Singleton Creation system
            SystemRegistry.AddSystem(new SingletonCreationSystem(this));

            //Creation System
            SystemRegistry.AddSystem(
                new MusicNoteCreationSystem(musicNoteCreationSettings, generalGameSetting)
            );
            SystemRegistry.AddSystem(new ScoreEffectCreationSystem(this));

            //Handling Data system
            SystemRegistry.AddSystem(new MovingNoteSystem(generalGameSetting));
            SystemRegistry.AddSystem(new InputSystem());
            SystemRegistry.AddSystem(new InputCollisionSystem(generalGameSetting));
            SystemRegistry.AddSystem(new ScoringSystem(this));
            SystemRegistry.AddSystem(new ScoreEffectSystem(this));
            SystemRegistry.AddSystem(new ProgressSystem(this));

            //Syncer systems
            SystemRegistry.AddSystem(new MusicNoteSyncer(this));
            SystemRegistry.AddSystem(new StartingNoteSyncer(this));
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

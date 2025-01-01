using UnityEngine;

namespace ECS_MagicTile
{
    public class GlobalPoint : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField]
        private GeneralGameSetting generalGameSetting;

        [SerializeField]
        private MusicNoteCreationSetting musicNoteCreationSettings;

        [SerializeField]
        private PerfectLineSettingSO perfectLineSettingSO;

        private World world;

        private void Awake()
        {
            // Initialize our ECS world
            world = new World();

            CreateSingletonComponent();

            SystemRegistry.Initialize(world);
            RegisterSystems();
        }

        private void RegisterSystems()
        {
            SystemRegistry.AddSystem(
                new MusicNoteCreationSystem_(musicNoteCreationSettings, generalGameSetting)
            );
        }

        private void CreateSingletonComponent()
        {
            var components = new object[] { new PerfectLineTagComponent(), new CornerComponent() };
            world.CreateEntityWithComponents(Archetype.Registry.PerfectLine, components);

            ArchetypeStorage perfectLineStorage = world.GetStorage(Archetype.Registry.PerfectLine);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            ref CornerComponent perfectLineCorner =
                ref perfectLineStorage.GetComponents<CornerComponent>()[0];

            PerfectLine.PerfectLineWidth = perfectLineSettingSO.PerfectLineWidth();
            perfectLineCorner.TopLeft = perfectLineSettingSO.TopLeft;
            perfectLineCorner.TopRight = perfectLineSettingSO.TopRight;
            perfectLineCorner.BottomLeft = perfectLineSettingSO.BottomLeft;
            perfectLineCorner.BottomRight = perfectLineSettingSO.BottomRight;
        }

        private void Update()
        {
            // Update all systems with current frame's delta time
            SystemRegistry.Update(Time.deltaTime);
        }
    }
}

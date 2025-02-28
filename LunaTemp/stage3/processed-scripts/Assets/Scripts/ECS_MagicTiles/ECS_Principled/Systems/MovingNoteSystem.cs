using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MovingNoteSystem : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.IngamePlaying;

        private readonly GeneralGameSetting generalGameSetting;
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;

        ArchetypeStorage musicNoteStorage;

        TransformComponent[] musicNoteTransforms;
        CornerComponent[] musicNoteCornsers;
        MusicNoteComponent[] musicNoteComponents;

        private readonly MusicNoteViewSyncTool musicNoteViewSyncTool;

        public MovingNoteSystem(GlobalPoint globalPoint)
        {
            this.generalGameSetting = globalPoint.generalGameSetting;
            this.musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            musicNoteViewSyncTool = globalPoint.musicNoteViewSyncTool;
        }

        public void RunCleanup() { }

        public void RunInitialize()
        {
            musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);

            musicNoteTransforms = musicNoteStorage.GetComponents<TransformComponent>();
            musicNoteCornsers = musicNoteStorage.GetComponents<CornerComponent>();
            musicNoteComponents = musicNoteStorage.GetComponents<MusicNoteComponent>();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime)
        {
            float gameSpeed;

            if (musicNoteCreationSetting.UsePreciseNoteCalculation)
            {
                gameSpeed = generalGameSetting.PreciseGameSpeed;
            }
            else
            {
                gameSpeed = generalGameSetting.GameSpeed;
            }

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                if (
                    musicNoteComponents[i].musicNotePositionState
                    == MusicNotePositionState.OutOfScreen
                )
                {
                    continue;
                }

                // Update position
                Vector2 newPos = musicNoteTransforms[i].Position;
                newPos.y -= gameSpeed * Time.deltaTime;
                musicNoteTransforms[i].Position = newPos;

                // Update corners based on new position and size
                Vector2 halfSize = musicNoteTransforms[i].Size * 0.5f;
                musicNoteCornsers[i].TopLeft = new Vector2(
                    newPos.x - halfSize.x,
                    newPos.y + halfSize.y
                );
                musicNoteCornsers[i].TopRight = new Vector2(
                    newPos.x + halfSize.x,
                    newPos.y + halfSize.y
                );
                musicNoteCornsers[i].BottomLeft = new Vector2(
                    newPos.x - halfSize.x,
                    newPos.y - halfSize.y
                );
                musicNoteCornsers[i].BottomRight = new Vector2(
                    newPos.x + halfSize.x,
                    newPos.y - halfSize.y
                );

                if (
                    CameraViewUtils.IsPositionOutOfBounds(
                        Camera.main,
                        musicNoteCornsers[i].TopLeft,
                        CameraViewUtils.CameraBoundCheck.Bottom
                    )
                )
                {
                    musicNoteComponents[i].musicNotePositionState =
                        MusicNotePositionState.OutOfScreen;
                }
            }

            musicNoteViewSyncTool.SyncNoteTransforms(musicNoteTransforms, musicNoteComponents);

            //SystemRegistry.SetGameState(EGameState.Outro);
        }
    }
}

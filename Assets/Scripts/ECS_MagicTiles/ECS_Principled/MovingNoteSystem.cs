using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MovingNoteSystem : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        private readonly GeneralGameSetting generalGameSetting;

        public MovingNoteSystem(GeneralGameSetting generalGameSetting)
        {
            this.generalGameSetting = generalGameSetting;
        }

        public void Cleanup() { }

        public void Initialize() { }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            Vector2 newPos = Vector2.zero;

            float gameSpeed = generalGameSetting.GameSpeed;

            ArchetypeStorage musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);

            TransformComponent[] transforms = musicNoteStorage.GetComponents<TransformComponent>();

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                newPos.x = transforms[i].Posision.x;
                newPos.y = transforms[i].Posision.y - gameSpeed * Time.deltaTime;

                transforms[i].Posision = newPos;
            }
        }
    }
}

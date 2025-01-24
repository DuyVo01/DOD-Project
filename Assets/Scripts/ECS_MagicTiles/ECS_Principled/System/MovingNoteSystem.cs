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
            ArchetypeStorage musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);

            TransformComponent[] transforms = musicNoteStorage.GetComponents<TransformComponent>();
            CornerComponent[] corners = musicNoteStorage.GetComponents<CornerComponent>();

            float gameSpeed = generalGameSetting.GameSpeed;

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                // Update position
                Vector2 newPos = transforms[i].Posision;
                newPos.y -= gameSpeed * Time.deltaTime;
                transforms[i].Posision = newPos;

                // Update corners based on new position and size
                Vector2 halfSize = transforms[i].Size * 0.5f;
                corners[i].TopLeft = new Vector2(newPos.x - halfSize.x, newPos.y + halfSize.y);
                corners[i].TopRight = new Vector2(newPos.x + halfSize.x, newPos.y + halfSize.y);
                corners[i].BottomLeft = new Vector2(newPos.x - halfSize.x, newPos.y - halfSize.y);
                corners[i].BottomRight = new Vector2(newPos.x + halfSize.x, newPos.y - halfSize.y);
            }
        }
    }
}

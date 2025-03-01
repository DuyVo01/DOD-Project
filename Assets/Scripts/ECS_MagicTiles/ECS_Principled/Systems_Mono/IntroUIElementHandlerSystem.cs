using ComponentCache;
using UnityEngine;

namespace ECS_MagicTile
{
    public class IntroUIElementHandlerSystem : MonoBehaviour, IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize() { }

        public void RunUpdate(float deltaTime) { }

        public void RunCleanup() { }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public abstract class ArchetypeSyncer : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        protected ArchetypeStorage DedicatedStorage => World.GetStorage(Archetype);
        protected abstract Archetype Archetype { get; }

        public void SetWorld(World world)
        {
            World = world;
        }

        public virtual void Initialize() { }

        public virtual void Update(float deltaTime) { }

        public virtual void Cleanup() { }

        protected abstract void SyncEntityToView(int entityId, GameObject view);
    }
}

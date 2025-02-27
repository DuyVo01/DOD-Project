using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public abstract class ArchetypeSyncer : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        protected ArchetypeStorage DedicatedStorage => World.GetStorage(Archetype);
        protected abstract Archetype Archetype { get; }

        public abstract EGameState GameStateToExecute { get; }

        public void SetWorld(World world)
        {
            World = world;
        }

        public virtual void RunInitialize() { }

        public virtual void RunUpdate(float deltaTime) { }

        public virtual void RunCleanup() { }
    }
}

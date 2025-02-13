using UnityEngine;

namespace ECS_MagicTile
{
    public abstract class BaseSyncTool
    {
        protected World World;
        protected abstract Archetype Archetype { get; }
        protected ArchetypeStorage DedicatedStorage;

        public bool IsInitialized { get; set; }

        public BaseSyncTool(GlobalPoint globalPoint)
        {
            this.World = globalPoint.World;

            DedicatedStorage = World.GetStorage(Archetype);

            IsInitialized = false;
        }

        public virtual void InitializeTool()
        {
            if (IsInitialized)
                return;
            IsInitialized = true;
        }
    }
}

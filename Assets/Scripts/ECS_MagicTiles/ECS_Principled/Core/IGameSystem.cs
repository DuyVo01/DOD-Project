using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// Interface for all game systems in the ECS architecture
    /// </summary>
    public interface IGameSystem
    {
        /// <summary>
        /// Sets the world for this system
        /// </summary>
        void SetWorld(World world);
        
        /// <summary>
        /// Called when the system is first created
        /// </summary>
        void RunInitialize();
        
        /// <summary>
        /// Called every frame to update the system
        /// </summary>
        void RunUpdate(float deltaTime);
        
        /// <summary>
        /// Called when the system is being destroyed
        /// </summary>
        void RunCleanup();
        
        /// <summary>
        /// Controls if the system should be updated
        /// </summary>
        bool IsEnabled { get; set; }
        
        /// <summary>
        /// Reference to the world this system belongs to
        /// </summary>
        World World { get; set; }
    }
    
    /// <summary>
    /// Base implementation of IGameSystem to reduce boilerplate
    /// </summary>
    public abstract class GameSystemBase : IGameSystem
    {
        public World World { get; set; }
        public bool IsEnabled { get; set; } = true;
        
        public virtual void SetWorld(World world)
        {
            World = world;
        }
        
        public virtual void RunInitialize()
        {
            Initialize();
        }
        
        public virtual void RunUpdate(float deltaTime)
        {
            if (IsEnabled)
            {
                Execute(deltaTime);
            }
        }
        
        public virtual void RunCleanup()
        {
            Cleanup();
        }
        
        /// <summary>
        /// Override this method to implement initialization logic
        /// </summary>
        protected virtual void Initialize() { }
        
        /// <summary>
        /// Override this method to implement update logic
        /// </summary>
        protected abstract void Execute(float deltaTime);
        
        /// <summary>
        /// Override this method to implement cleanup logic
        /// </summary>
        protected virtual void Cleanup() { }
        
        /// <summary>
        /// Creates a query for the world
        /// </summary>
        protected EntityQuery CreateQuery()
        {
            return World.CreateQuery();
        }
    }
}
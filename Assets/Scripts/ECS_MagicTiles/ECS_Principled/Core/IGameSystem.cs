using UnityEngine;

namespace ECS_MagicTile
{
    // Interface that all our game systems will implement

    public interface IGameSystem
    {
        void SetWorld(World world);
        void RunInitialize(); // Called when system is first created
        void RunUpdate(float deltaTime); // Called every frame
        void RunCleanup();
        bool IsEnabled { get; set; } // Controls if system should be updated
        World World { get; set; }
        EGameState GameStateToExecute { get; }
    }
}

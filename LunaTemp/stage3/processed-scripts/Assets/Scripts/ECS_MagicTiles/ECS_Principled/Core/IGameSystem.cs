using UnityEngine;

namespace ECS_MagicTile
{
    // Interface that all our game systems will implement

    public interface IGameSystem
    {
        void SetWorld(World world);
        void Initialize(); // Called when system is first created
        void Update(float deltaTime); // Called every frame
        void Cleanup();
        bool IsEnabled { get; set; } // Controls if system should be updated
        World World { get; set; }
        EGameState GameStateToExecute { get; }
    }
}

using System.Collections.Generic;
using StateMachineChart;
using UnityEngine;

namespace ECS_MagicTile
{
    public class GameSystemState : BaseState
    {
        protected IGameSystem[] systems;

        public GameSystemState(World world, IGameSystem[] systems)
        {
            this.systems = systems;

            foreach (var system in systems)
            {
                system.SetWorld(world);
            }
        }

        public override void Enter()
        {
            base.Enter();
            foreach (var system in systems)
            {
                system.RunInitialize();
            }
        }

        public override void Update()
        {
            base.Update();
            // Update all systems
            foreach (var system in systems)
            {
                if (system.IsEnabled)
                {
                    try
                    {
                        system.RunUpdate(Time.deltaTime);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Error in system {system.GetType().Name}: {e}");
                    }
                }
            }
        }

        public override void Exit()
        {
            // Clean up when leaving the state
            foreach (var system in systems)
            {
                system.RunCleanup();
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public class EntityViewManager : MonoBehaviour, IGameSystem
    {
        const int DEFAULT_FACTORY_COUNT = 8;
        private Dictionary<int, int> factoryIdToIndex = new Dictionary<int, int>();
        private EntityViewFactory[] entityViewFactories;
        Queue<int> holeIndicies = new Queue<int>();

        int highestIndex = 0;

        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.All;

        public void RegisterEntityViewFactory(int factoryId, EntityViewFactory factory)
        {
            if (holeIndicies.Count > 0)
            {
                int holeIndex = holeIndicies.Dequeue();
                entityViewFactories[holeIndex] = factory;
                factoryIdToIndex[factoryId] = holeIndex;
                return;
            }
            factoryIdToIndex[factoryId] = highestIndex;
            entityViewFactories[highestIndex] = factory;

            highestIndex++;
        }

        public void RemoveEntityViewFactory(int factoryId)
        {
            int factoryIndex = factoryIdToIndex[factoryId];
            entityViewFactories[factoryIndex] = null;
            factoryIdToIndex.Remove(factoryId);
            holeIndicies.Enqueue(factoryIndex);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize()
        {
            entityViewFactories = new EntityViewFactory[DEFAULT_FACTORY_COUNT];
        }

        public void RunUpdate(float deltaTime) { }

        public void RunCleanup() { }
    }
}

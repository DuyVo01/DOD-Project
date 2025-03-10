using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteSyncTool : BaseSyncTool
    {
        public StartingNoteSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
        {
            startingNoteFactory = new EntityViewFactory(
                globalPoint.musicNoteCreationSettings.startingNotePrefab,
                globalPoint.transform
            );
        }

        protected override Archetype Archetype => Archetype.Registry.StartingNote;

        private readonly EntityViewFactory startingNoteFactory;
        private GameObject startingNoteViewObject;

        public override void InitializeTool()
        {
            base.InitializeTool();
            
            // Get the singleton entity ID
            int entityId = World.GetSingletonEntityId<StartingNoteTagComponent>();
            
            // Create or get the view object
            startingNoteViewObject = startingNoteFactory.GetOrCreateView(entityId, "StartingNote");
        }

        public void SyncStartNoteTransform(TransformComponent startNoteTransform)
        {
            startingNoteViewObject.transform.position = startNoteTransform.Position;
            startingNoteViewObject.transform.localScale = startNoteTransform.Size;
        }

        public void SyncStartNoteState(ActiveStateComponent startNoteState)
        {
            startingNoteViewObject.SetActive(startNoteState.IsActive);
        }
        
        /// <summary>
        /// Syncs the starting note using singleton references
        /// </summary>
        public void SyncSingleton()
        {
            // Get direct references to the singleton components
            ref var transform = ref World.GetSingleton<StartingNoteTagComponent, TransformComponent>();
            ref var activeState = ref World.GetSingleton<StartingNoteTagComponent, ActiveStateComponent>();
            
            // Update the visual representation
            startingNoteViewObject.transform.position = transform.Position;
            startingNoteViewObject.transform.localScale = transform.Size;
            startingNoteViewObject.SetActive(activeState.IsActive);
        }
    }
}

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
            int entityId_ = DedicatedStorage.EntityIds[0];

            startingNoteViewObject = startingNoteFactory.GetOrCreateView(entityId_, "StartingNote");
        }

        public void SyncStartNoteTransform(TransformComponent startNoteTransform)
        {
            startingNoteViewObject.transform.position = startNoteTransform.Posision;
            startingNoteViewObject.transform.localScale = startNoteTransform.Size;
        }

        public void SyncStartNoteState(ActiveStateComponent startNoteState)
        {
            startingNoteViewObject.SetActive(startNoteState.isActive);
        }
    }
}

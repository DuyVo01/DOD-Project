using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteSyncer : ArchetypeSyncer
    {
        protected override Archetype Archetype => Archetype.Registry.StartingNote;

        public override EGameState GameStateToExecute => EGameState.All;

        private readonly EntityViewFactory startingNoteFactory;

        ActiveStateComponent[] startingNoteActiveStateComponents;
        TransformComponent[] startingNoteTransformComponents;

        private GameObject startingNoteViewObject;

        public StartingNoteSyncer(GlobalPoint globalPoint)
        {
            startingNoteFactory = new EntityViewFactory(
                globalPoint.musicNoteCreationSettings.startingNotePrefab,
                globalPoint.transform
            );
        }

        public override void Initialize()
        {
            base.Initialize();
            IsEnabled = true;

            startingNoteActiveStateComponents =
                DedicatedStorage.GetComponents<ActiveStateComponent>();

            startingNoteTransformComponents = DedicatedStorage.GetComponents<TransformComponent>();

            int entityId_ = DedicatedStorage.EntityIds[0];

            startingNoteViewObject = startingNoteFactory.GetOrCreateView(entityId_, "StartingNote");
            startingNoteViewObject.transform.localScale = startingNoteTransformComponents[0].Size;
        }

        public override void Update(float deltaTime)
        {
            startingNoteViewObject.SetActive(startingNoteActiveStateComponents[0].isActive);
            // if (!startingNoteViewObject.activeSelf)
            // {
            //     IsEnabled = false;
            // }

            startingNoteViewObject.transform.position = startingNoteTransformComponents[0].Posision;
            startingNoteViewObject.transform.localScale = startingNoteTransformComponents[0].Size;
        }
    }
}

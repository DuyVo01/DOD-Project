using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteSyncer : ArchetypeSyncer
    {
        protected override Archetype Archetype => Archetype.Registry.StartingNote;

        public override EGameState GameStateToExecute => EGameState.Ingame;

        private readonly EntityViewFactory startingNoteFactory;

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

            TransformComponent startingNoteTransform =
                DedicatedStorage.GetComponents<TransformComponent>()[0];

            int entityId_ = DedicatedStorage.EntityIds[0];

            startingNoteViewObject = startingNoteFactory.GetOrCreateView(entityId_, "StartingNote");

            startingNoteViewObject.transform.position = startingNoteTransform.Posision;
            startingNoteViewObject.transform.localScale = startingNoteTransform.Size;
        }

        public override void Update(float deltaTime)
        {
            ActiveStateComponent activeStateComponent =
                DedicatedStorage.GetComponents<ActiveStateComponent>()[0];
            startingNoteViewObject.SetActive(activeStateComponent.isActive);
        }
    }
}

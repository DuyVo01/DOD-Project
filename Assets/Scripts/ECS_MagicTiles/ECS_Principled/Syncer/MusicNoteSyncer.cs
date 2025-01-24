using System;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MusicNoteSyncer : ArchetypeSyncer
    {
        protected override Archetype Archetype => Archetype.Registry.MusicNote;

        private readonly EntityViewFactory shortNoteViewFactory;
        private readonly EntityViewFactory longNoteViewFactory;

        public MusicNoteSyncer(
            GameObject shortNotePrefab,
            GameObject longNotePrefab,
            Transform root = null
        )
        {
            shortNoteViewFactory = new EntityViewFactory(shortNotePrefab, root);
            longNoteViewFactory = new EntityViewFactory(longNotePrefab, root);
        }

        public override void Initialize()
        {
            base.Initialize();
            IsEnabled = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var notes = DedicatedStorage.GetComponents<MusicNoteComponent>();
            var interactions = DedicatedStorage.GetComponents<MusicNoteInteractionComponent>();
            var fillers = DedicatedStorage.GetComponents<MusicNoteFillerComponent>();

            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];
                GameObject view;

                // Get or create appropriate view based on note type
                if (notes[i].musicNoteType == MusicNoteType.LongNote)
                {
                    view = longNoteViewFactory.GetOrCreateView(entityId, "longNote");
                }
                else
                {
                    view = shortNoteViewFactory.GetOrCreateView(entityId, "shortNote");
                }

                // Basic transform sync
                SyncEntityToView(entityId, view);

                // State sync
                SyncNoteState(view, notes[i], interactions[i], fillers[i]);
            }
        }

        protected override void SyncEntityToView(int entityId, GameObject view)
        {
            TransformComponent[] transforms = DedicatedStorage.GetComponents<TransformComponent>();
            int index = Array.IndexOf(DedicatedStorage.EntityIds.ToArray(), entityId);

            if (index != -1)
            {
                view.transform.position = transforms[index].Posision;
                view.transform.localScale = transforms[index].Size;
                // Sync other visual properties specific to MusicNote
            }
        }

        private void SyncNoteState(
            GameObject view,
            MusicNoteComponent note,
            MusicNoteInteractionComponent interaction,
            MusicNoteFillerComponent filler
        )
        {
            // Get required components from view
            var noteRenderer = view.GetComponent<SpriteRenderer>();
            var fillerObject = view.transform.Find("Filler")?.gameObject;
            var fillerRenderer = fillerObject?.GetComponent<SpriteRenderer>();

            // Update visual state based on interaction state
            switch (interaction.State)
            {
                case MusicNoteInteractiveState.Normal:
                    noteRenderer.color = Color.white; // Default state
                    break;
                case MusicNoteInteractiveState.Pressed:
                case MusicNoteInteractiveState.Hold:
                    noteRenderer.color = Color.yellow; // Active state
                    break;
                case MusicNoteInteractiveState.Completed:
                    noteRenderer.color = new Color(1, 1, 1, 0.5f); // Fade out completed notes
                    break;
            }

            // Handle long note filler
            if (
                note.musicNoteType == MusicNoteType.LongNote
                && fillerObject != null
                && fillerRenderer != null
            )
            {
                fillerObject.SetActive(filler.IsVisible);
                if (filler.IsVisible)
                {
                    // Update filler visualization
                    UpdateNoteFiller(fillerObject, fillerRenderer, filler.FillPercent);
                }
            }
        }

        private void UpdateNoteFiller(
            GameObject fillerObj,
            SpriteRenderer fillerRenderer,
            float fillPercent
        )
        {
            // Assuming filler uses a scale-based filling mechanism
            Vector3 scale = fillerObj.transform.localScale;
            scale.y = fillPercent;
            // fillerObj.transform.localScale = scale;

            SpriteUtility.ScaleFromPivot(fillerRenderer, scale, SpriteUtility.PivotPointXY.Bottom);

            // Optional: Color gradient based on fill percentage
            fillerRenderer.color = Color.Lerp(Color.yellow, Color.green, fillPercent);
        }
    }
}

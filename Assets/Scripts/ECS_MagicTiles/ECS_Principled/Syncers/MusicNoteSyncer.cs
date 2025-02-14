using System;
using System.Collections.Generic;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MusicNoteSyncer : ArchetypeSyncer
    {
        protected override Archetype Archetype => Archetype.Registry.MusicNote;

        public override EGameState GameStateToExecute { get; } = EGameState.Ingame;

        private readonly EntityViewFactory shortNoteViewFactory;
        private readonly EntityViewFactory longNoteViewFactory;

        // Cache component arrays to avoid getting them each frame
        private TransformComponent[] transforms;
        private MusicNoteComponent[] notes;
        private MusicNoteInteractionComponent[] interactions;
        private MusicNoteFillerComponent[] fillers;

        // Cache frequently accessed components
        private readonly Dictionary<int, SpriteRenderer> noteRenderers = new();
        private readonly Dictionary<int, (GameObject obj, SpriteRenderer renderer)> fillerCache =
            new();

        public MusicNoteSyncer(GlobalPoint globalPoint)
        {
            shortNoteViewFactory = new EntityViewFactory(
                globalPoint.musicNoteCreationSettings.ShortTilePrefab,
                globalPoint.transform
            );
            longNoteViewFactory = new EntityViewFactory(
                globalPoint.musicNoteCreationSettings.LongTilePrefab,
                globalPoint.transform
            );
        }

        public override void Initialize()
        {
            IsEnabled = true;

            // Initialize component array references
            transforms = DedicatedStorage.GetComponents<TransformComponent>();
            notes = DedicatedStorage.GetComponents<MusicNoteComponent>();
            interactions = DedicatedStorage.GetComponents<MusicNoteInteractionComponent>();
            fillers = DedicatedStorage.GetComponents<MusicNoteFillerComponent>();
        }

        public override void Update(float deltaTime)
        {
            // Process notes in batches for better cache utilization
            const int BATCH_SIZE = 64;
            int totalNotes = DedicatedStorage.Count;

            for (int batchStart = 0; batchStart < totalNotes; batchStart += BATCH_SIZE)
            {
                int batchEnd = Math.Min(batchStart + BATCH_SIZE, totalNotes);
                ProcessNoteBatch(batchStart, batchEnd);
            }
        }

        private void ProcessNoteBatch(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];

                // Get or create view based on note type
                GameObject view = GetOrCreateNoteView(entityId, notes[i].musicNoteType);

                view.transform.position = transforms[i].Position;
                view.transform.localScale = transforms[i].Size;

                SyncNoteState(entityId, view, notes[i], interactions[i], fillers[i]);
            }
        }

        private GameObject GetOrCreateNoteView(int entityId, MusicNoteType noteType)
        {
            return noteType == MusicNoteType.LongNote
                ? longNoteViewFactory.GetOrCreateView(entityId, "longNote")
                : shortNoteViewFactory.GetOrCreateView(entityId, "shortNote");
        }

        private void SyncNoteState(
            int entityId,
            GameObject view,
            MusicNoteComponent note,
            MusicNoteInteractionComponent interaction,
            MusicNoteFillerComponent filler
        )
        {
            // Get cached renderer or cache it
            if (!noteRenderers.TryGetValue(entityId, out var noteRenderer))
            {
                noteRenderer = view.GetComponent<SpriteRenderer>();
                noteRenderers[entityId] = noteRenderer;
            }

            // Update note color based on state
            UpdateNoteColor(noteRenderer, interaction.State);

            // Handle long note filler
            if (note.musicNoteType == MusicNoteType.LongNote)
            {
                SyncNoteFiller(entityId, view, filler);
            }
        }

        private static void UpdateNoteColor(
            SpriteRenderer renderer,
            MusicNoteInteractiveState state
        )
        {
            renderer.color = state switch
            {
                MusicNoteInteractiveState.Normal => Color.white,
                MusicNoteInteractiveState.Pressed or MusicNoteInteractiveState.Hold => Color.yellow,
                MusicNoteInteractiveState.Completed => new Color(1, 1, 1, 0.5f),
                _ => Color.white,
            };
        }

        private void SyncNoteFiller(int entityId, GameObject view, MusicNoteFillerComponent filler)
        {
            if (!fillerCache.TryGetValue(entityId, out var fillerComponents))
            {
                var fillerObj = view.transform.Find("Filler")?.gameObject;
                if (fillerObj != null)
                {
                    fillerComponents = (fillerObj, fillerObj.GetComponent<SpriteRenderer>());
                    fillerCache[entityId] = fillerComponents;
                }
            }

            if (fillerComponents.obj != null)
            {
                fillerComponents.obj.SetActive(filler.IsVisible);
                if (filler.IsVisible)
                {
                    UpdateFillerVisuals(
                        fillerComponents.obj,
                        fillerComponents.renderer,
                        filler.FillPercent
                    );
                }
            }
        }

        private static void UpdateFillerVisuals(
            GameObject fillerObj,
            SpriteRenderer renderer,
            float fillPercent
        )
        {
            Vector3 scale = fillerObj.transform.localScale;
            scale.y = fillPercent;
            SpriteUtility.ScaleFromPivot(renderer, scale, SpriteUtility.PivotPointXY.Bottom);
            renderer.color = Color.Lerp(Color.yellow, Color.green, fillPercent);
        }
    }
}

using System.Collections.Generic;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MusicNoteViewSyncTool : BaseSyncTool
    {
        private readonly EntityViewFactory shortNoteViewFactory;
        private readonly EntityViewFactory longNoteViewFactory;
        protected override Archetype Archetype => Archetype.Registry.MusicNote;

        private readonly Dictionary<int, SpriteRenderer> noteRenderers = new System.Collections.Generic.Dictionary<int, UnityEngine.SpriteRenderer>();
        private readonly Dictionary<int, (GameObject obj, SpriteRenderer renderer)> fillerCache =
            new System.Collections.Generic.Dictionary<int, (UnityEngine.GameObject obj, UnityEngine.SpriteRenderer renderer)>();

        public MusicNoteViewSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
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

        private GameObject GetOrCreateNoteView(int entityId, MusicNoteType noteType)
        {
            return noteType == MusicNoteType.LongNote
                ? longNoteViewFactory.GetOrCreateView(entityId, "longNote")
                : shortNoteViewFactory.GetOrCreateView(entityId, "shortNote");
        }

        public void SyncNoteTransforms(TransformComponent[] transforms, MusicNoteComponent[] notes)
        {
            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];
                GameObject view = GetOrCreateNoteView(entityId, notes[i].musicNoteType);

                view.transform.position = transforms[i].Position;
                view.transform.localScale = transforms[i].Size;
            }
        }

        public void SyncNoteState(
            MusicNoteInteractionComponent[] interaction,
            MusicNoteFillerComponent[] fillers,
            MusicNoteComponent[] notes
        )
        {
            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];
                GameObject view = GetOrCreateNoteView(entityId, notes[i].musicNoteType);

                if (!noteRenderers.TryGetValue(entityId, out var noteRenderer))
                {
                    noteRenderer = view.GetComponent<SpriteRenderer>();
                    noteRenderers[entityId] = noteRenderer;
                }

                // Update note color based on state
                UpdateNoteColor(noteRenderer, interaction[i].State);

                // Handle long note filler
                if (notes[i].musicNoteType == MusicNoteType.LongNote)
                {
                    SyncNoteFiller(entityId, view, fillers[i]);
                }
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

        public void SyncNoteFiller(int entityId, GameObject view, [Bridge.Ref] MusicNoteFillerComponent filler)
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

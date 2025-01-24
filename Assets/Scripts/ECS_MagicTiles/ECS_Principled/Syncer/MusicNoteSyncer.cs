using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
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
            MusicNoteComponent[] notes = DedicatedStorage.GetComponents<MusicNoteComponent>();

            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];
                GameObject view;
                if (notes[i].musicNoteType == MusicNoteType.LongNote)
                {
                    view = longNoteViewFactory.GetOrCreateView(entityId, "longNote");
                }
                else
                {
                    view = shortNoteViewFactory.GetOrCreateView(entityId, "shortNote");
                }

                SyncEntityToView(entityId, view);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net;
using ECS_Core;
using UnityEngine;

public struct MusicNoteCreationSystem : ECS_Core.IGameSystem
{
    public bool AutoUpdate { get; set; }
    public ArchetypeManager ArchetypeManager { get; set; }

    public void Cleanup()
    {
        //
    }

    public void Initialize()
    {
        AutoUpdate = false;
    }

    public void Start()
    {
        //
        MusicNoteMidiData musicNoteMidiData = MidiNoteParser.ParseFromText(
            GlobalPoint.Instance.midiContent.text
        );

        Debug.Log($"Total Notes; {musicNoteMidiData.TotalNotes}");

        var entitiesToUpdate = new List<int>();

        World.Active.GetSingletonComponents<PerfectLineTagComponent, CornerComponent>(
            out CornerComponent perfectLineCorner
        );

        Debug.Log("Perfect Line topleft: " + perfectLineCorner.TopLeft);

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            int noteEntity = World.Active.CreateEntityFromTemplate("MusicNote");
            World.Active.ModifyPendingComponent(
                noteEntity,
                (ref MusicNoteComponent component) =>
                {
                    component.Duration = musicNoteMidiData.Durations[i];
                    component.PostionId = musicNoteMidiData.PositionIds[i];
                    component.TimeAppear = musicNoteMidiData.TimeAppears[i];

                    Debug.Log($"PosId of Entity {noteEntity}: {component.PostionId}");
                    Debug.Log($"Duration of Entity {noteEntity}: {component.Duration}");
                    Debug.Log($"TimeAppear of Entity {noteEntity}: {component.TimeAppear}");
                }
            );

            entitiesToUpdate.Add(noteEntity);
        }

        World.Active.UpdateEntities(entitiesToUpdate);
    }

    public void Update()
    {
        //
    }
}

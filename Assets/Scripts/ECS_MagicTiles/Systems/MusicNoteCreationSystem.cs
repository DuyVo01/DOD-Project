using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using ECS_Core;
using UnityEngine;

public struct MusicNoteCreationSystem : ECS_Core.IGameSystem
{
    public bool AutoUpdate { get; set; }
    public ArchetypeManager ArchetypeManager { get; set; }

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

        var entitiesToUpdate = new List<int>();

        World.Active.GetSingletonComponents<PerfectLineTagComponent, CornerComponent>(
            out CornerComponent perfectLine
        );

        Debug.Log("Perfect Line topleft: " + perfectLine.TopLeft);

        // Calculate lane width once
        float totalWidth = perfectLine.TopRight.x - perfectLine.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            int noteEntity = World.Active.CreateEntityFromTemplate("MusicNote");
            Debug.Log($"Created entity {noteEntity}");
            World.Active.ModifyPendingComponent(
                noteEntity,
                (ref MusicNoteComponent component) =>
                {
                    component.Duration = musicNoteMidiData.Durations[i];
                    component.PostionId = musicNoteMidiData.PositionIds[i];
                    component.TimeAppear = musicNoteMidiData.TimeAppears[i];
                }
            );

            World.Active.ModifyPendingComponent(
                noteEntity,
                (ref TransformComponent component) =>
                {
                    float spawnX =
                        perfectLine.TopLeft.x
                        + (musicNoteMidiData.PositionIds[i] * laneWidth)
                        + halfLaneWidth;

                    float spawnY =
                        perfectLine.TopLeft.y
                        + (musicNoteMidiData.TimeAppears[i] * GlobalPoint.Instance.gameSpeed)
                        + component.Size.y / 2f;

                    component.Posision = new Vector2(spawnX, spawnY);
                }
            );

            if (
                musicNoteMidiData
                    .Durations[i]
                    .IsInRange(musicNoteMidiData.MinDuration, musicNoteMidiData.MinDuration + 0.01f)
            )
            {
                World.Active.AddComponent(noteEntity, new ShortNoteTagComponent());
            }
            else if (musicNoteMidiData.Durations[i] > musicNoteMidiData.MinDuration)
            {
                World.Active.AddComponent(noteEntity, new LongNoteTagComponent());
            }

            entitiesToUpdate.Add(noteEntity);
        }

        World.Active.UpdateEntities(entitiesToUpdate);

        World
            .Query<MusicNoteComponent, TransformComponent>()
            .ForEach(
                World.Active,
                (
                    int entityId,
                    ref MusicNoteComponent musicNoteComponent,
                    ref TransformComponent transformComponent
                ) =>
                {
                    Debug.Log("entityId: " + entityId);
                }
            );
    }

    public void Update()
    {
        //
    }

    public void Cleanup()
    {
        //
    }
}

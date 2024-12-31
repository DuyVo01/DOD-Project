using System.Collections.Generic;
using ECS_Core;
using UnityEngine;

public class MusicNoteCreationSystem : ECS_Core.IGameSystem
{
    private MusicNoteCreationSettings musicNoteCreationSettings;

    public MusicNoteCreationSystem(MusicNoteCreationSettings musicNoteCreationSettings)
    {
        this.musicNoteCreationSettings = musicNoteCreationSettings;
    }

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
            out PerfectLineTagComponent perfectLineTagComponent,
            out CornerComponent perfectLineCorners
        );

        Debug.Log("Perfect Line topleft: " + perfectLineCorners.TopLeft);

        // Calculate lane width once
        float totalWidth = perfectLineCorners.TopRight.x - perfectLineCorners.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            int noteEntity = World.Active.CreateEntityFromTemplate("MusicNote");

            World.Active.ModifyPendingComponent(
                noteEntity,
                (ref MusicNoteComponent component) =>
                {
                    if (
                        musicNoteMidiData
                            .Durations[i]
                            .IsInRange(
                                musicNoteMidiData.MinDuration,
                                musicNoteMidiData.MinDuration + 0.01f
                            )
                    )
                    {
                        component.musicNoteType = MusicNoteType.ShortNote;
                    }
                    else if (musicNoteMidiData.Durations[i] > musicNoteMidiData.MinDuration)
                    {
                        component.musicNoteType = MusicNoteType.LongNote;
                    }

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
                        perfectLineCorners.TopLeft.x
                        + (musicNoteMidiData.PositionIds[i] * laneWidth)
                        + halfLaneWidth;

                    float spawnY =
                        perfectLineCorners.TopLeft.y
                        + (musicNoteMidiData.TimeAppears[i] * GlobalPoint.Instance.gameSpeed)
                        + component.Size.y / 2f;

                    component.Posision = new Vector2(spawnX, spawnY);
                }
            );

            entitiesToUpdate.Add(noteEntity);
        }

        World.Active.UpdateEntities(entitiesToUpdate);

        World
            .Query<MusicNoteComponent, TransformComponent>()
            .ForEach(
                World.Active,
                (
                    int entity,
                    ref MusicNoteComponent musicNoteComponent,
                    ref TransformComponent transformComponent
                ) =>
                {
                    float scaleX = perfectLineTagComponent.perfectLineWidth / 4;
                    float scaleY = 0f;

                    if (musicNoteComponent.musicNoteType == MusicNoteType.ShortNote)
                    {
                        scaleY = MagicTileHelper.CalculateScaleY(
                            musicNoteCreationSettings.shortNoteScaleYFactor,
                            scaleX
                        );
                    }
                    else
                    {
                        scaleY = MagicTileHelper.CalculateScaleY(
                            musicNoteCreationSettings.longNoteScaleYFactor,
                            scaleX,
                            musicNoteComponent.Duration
                        );
                    }

                    transformComponent.Size = new Vector2(scaleX, scaleY);
                    Debug.Log($"Setting entity {entity} size to: {transformComponent.Size}");
                }
            );

        World
            .Query<TransformComponent>()
            .ForEach(
                World.Active,
                (int entity, ref TransformComponent transformComponent) =>
                {
                    Debug.Log($"Entity {entity} Size: {transformComponent.Size}");
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

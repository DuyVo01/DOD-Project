using System.Collections.Generic;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MusicNoteCreationSystem : GameSystemBase
    {
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly GeneralGameSetting generalGameSetting;

        MusicNoteMidiData musicNoteMidiData;

        // Reference to perfect line components
        PerfectLineTagComponent perfectLineTag;
        CornerComponent perfectLineCorner;

        Camera targetCamera;

        float lastPerfectLineTopLeftY;
        float lastPerfectLineTopLeftX;

        public MusicNoteCreationSystem(GlobalPoint globalPoint)
        {
            this.musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            this.generalGameSetting = globalPoint.generalGameSetting;
            targetCamera = globalPoint.mainCamera;
        }

        protected override void Initialize()
        {
            musicNoteMidiData = MidiNoteParser.ParseFromText(
                musicNoteCreationSetting.MidiContent.text
            );

            // Get references to components we'll need
            World
                .CreateQuery()
                .ForEach<PerfectLineTagComponent, CornerComponent>(
                    (ref PerfectLineTagComponent tag, ref CornerComponent corner) =>
                    {
                        perfectLineTag = tag;
                        perfectLineCorner = corner;
                    }
                );

            CreateMusicNotes();
        }

        protected override void Execute(float deltaTime)
        {
            // Check if perfect line position has changed
            float currentTopLeftY = 0;
            float currentTopLeftX = 0;

            World
                .CreateQuery()
                .ForEach<CornerComponent>(
                    (ref CornerComponent corner) =>
                    {
                        if (
                            corner.TopLeft.y != lastPerfectLineTopLeftY
                            || corner.TopLeft.x != lastPerfectLineTopLeftX
                        )
                        {
                            currentTopLeftY = corner.TopLeft.y;
                            currentTopLeftX = corner.TopLeft.x;
                        }
                    }
                );

            if (currentTopLeftY != 0 || currentTopLeftX != 0)
            {
                lastPerfectLineTopLeftY = currentTopLeftY;
                lastPerfectLineTopLeftX = currentTopLeftX;

                if (musicNoteCreationSetting.UsePreciseNoteCalculation)
                {
                    CalculateMusicNoteDataPrecisely();
                }
                else
                {
                    CalculateMusicNoteData();
                }
            }
        }

        protected override void Cleanup()
        {
            //
        }

        private void CreateMusicNotes()
        {
            var noteCount = musicNoteMidiData.TotalNotes;
            var durations = musicNoteMidiData.Durations;
            var positionIds = musicNoteMidiData.PositionIds;
            var timeAppears = musicNoteMidiData.TimeAppears;
            var minDuration = musicNoteMidiData.MinDuration;

            for (int i = 0; i < noteCount; i++)
            {
                var musicNoteType =
                    durations[i] > minDuration ? MusicNoteType.LongNote : MusicNoteType.ShortNote;

                World.CreateEntityWithArchetype(
                    Archetype.Registry.MusicNote,
                    new object[]
                    {
                        new TransformComponent(),
                        new MusicNoteComponent
                        {
                            Duration = durations[i],
                            PostionId = positionIds[i],
                            TimeAppear = timeAppears[i],
                            musicNoteType = musicNoteType,
                            musicNotePositionState = MusicNotePositionState.AbovePerfectLine,
                        },
                        new CornerComponent(),
                        new MusicNoteInteractionComponent(),
                        new MusicNoteFillerComponent(),
                        new ScoreStateComponent { HasBeenScored = false },
                    }
                );
            }
        }

        private void CalculateMusicNoteData()
        {
            // Check if we have valid references
            if (perfectLineCorner.TopLeft.x == 0 && perfectLineCorner.TopLeft.y == 0)
            {
                // Get latest values
                World
                    .CreateQuery()
                    .ForEach<PerfectLineTagComponent, CornerComponent>(
                        (ref PerfectLineTagComponent tag, ref CornerComponent corner) =>
                        {
                            perfectLineTag = tag;
                            perfectLineCorner = corner;
                        }
                    );
            }

            // Calculate lane width and half lane width
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            // Calculate Short and Long Note scale factors
            float shortNoteScaleYFactor = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.ShortNoteScaleYFactor,
                perfectLineTag.PerfectLineWidth / 4
            );
            float longNoteScaleYFactor = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.LongNoteScaleYFactor,
                perfectLineTag.PerfectLineWidth / 4
            );

            // Set all music note positions and sizes using the query system
            World
                .CreateQuery()
                .ForEach<TransformComponent, MusicNoteComponent>(
                    (ref TransformComponent transform, ref MusicNoteComponent note, int entityId) =>
                    {
                        float spawnX =
                            perfectLineCorner.TopLeft.x
                            + (note.PostionId * laneWidth)
                            + halfLaneWidth;

                        float spawnY =
                            perfectLineCorner.TopLeft.y
                            + (note.TimeAppear * generalGameSetting.GameSpeed)
                            + (
                                note.musicNoteType == MusicNoteType.ShortNote
                                    ? shortNoteScaleYFactor
                                    : longNoteScaleYFactor
                            );

                        transform.Position = new Vector2(spawnX, spawnY);

                        transform.Size = new Vector2(
                            perfectLineTag.PerfectLineWidth / 4,
                            note.musicNoteType == MusicNoteType.ShortNote
                                ? shortNoteScaleYFactor
                                : longNoteScaleYFactor
                        );
                    }
                );
        }

        private void CalculateMusicNoteDataPrecisely()
        {
            // Check if we have valid references
            if (perfectLineCorner.TopLeft.x == 0 && perfectLineCorner.TopLeft.y == 0)
            {
                // Get latest values
                World
                    .CreateQuery()
                    .ForEach<PerfectLineTagComponent, CornerComponent>(
                        (ref PerfectLineTagComponent tag, ref CornerComponent corner) =>
                        {
                            perfectLineTag = tag;
                            perfectLineCorner = corner;
                        }
                    );
            }

            float[] noteSizes = PreciseNoteCalculator.CalculateNoteSizes(
                musicNoteMidiData,
                musicNoteCreationSetting.ShortNoteScaleYFactor
            );

            float[] positions = PreciseNoteCalculator.CalculateInitialPositions(
                musicNoteMidiData,
                perfectLineCorner.TopLeft.y,
                noteSizes,
                musicNoteCreationSetting.ShortNoteScaleYFactor
            );

            // Update world state
            World
                .CreateQuery()
                .ForEach<WorldStateComponent>(
                    (ref WorldStateComponent state) =>
                    {
                        state.FirstNotePositionToTriggerSong = positions[0];
                    }
                );

            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            float cameraBoundYOffset = CameraViewUtils.GetPositionYInCameraView(targetCamera, 1);

            // We need to be able to associate each note with its index in the arrays
            int noteIndex = 0;
            World
                .CreateQuery()
                .ForEach<TransformComponent, MusicNoteComponent>(
                    (ref TransformComponent transform, ref MusicNoteComponent note) =>
                    {
                        if (noteIndex < positions.Length)
                        {
                            float spawnX =
                                perfectLineCorner.TopLeft.x
                                + (note.PostionId * laneWidth)
                                + halfLaneWidth;

                            transform.Position = new Vector2(
                                spawnX,
                                positions[noteIndex]
                                    + (cameraBoundYOffset - perfectLineCorner.TopLeft.y)
                            );

                            transform.Size = new Vector2(
                                perfectLineTag.PerfectLineWidth / 4,
                                noteSizes[noteIndex]
                            );

                            noteIndex++;
                        }
                    }
                );

            float totalTime = PreciseNoteCalculator.CalculateTotalSongDuration(musicNoteMidiData);
            float roadLength = PreciseNoteCalculator.CalculateRoadLength(
                noteSizes,
                musicNoteMidiData
            );

            generalGameSetting.PreciseGameSpeed = PreciseNoteCalculator.CalculateRequiredVelocity(
                totalTime,
                roadLength
            );
        }
    }
}

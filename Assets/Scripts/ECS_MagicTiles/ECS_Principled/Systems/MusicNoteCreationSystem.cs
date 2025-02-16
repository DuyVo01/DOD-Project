using System.Collections.Generic;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class MusicNoteCreationSystem : IGameSystem
    {
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly GeneralGameSetting generalGameSetting;
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.WaitingToStart;

        MusicNoteMidiData musicNoteMidiData;

        ArchetypeStorage perfectLineStorage;
        PerfectLineTagComponent[] perfectLineTag;
        CornerComponent[] perfectLineCorners;

        ArchetypeStorage musicNoteStorage;
        TransformComponent[] musicNoteTransforms;
        MusicNoteComponent[] musicNotes;

        float lastPerfectLineTopLeftY;
        float lastPerfectLineTopLeftX;

        public MusicNoteCreationSystem(
            MusicNoteCreationSetting musicNoteCreationSetting,
            GeneralGameSetting generalGameSetting
        )
        {
            this.musicNoteCreationSetting = musicNoteCreationSetting;
            this.generalGameSetting = generalGameSetting;
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Initialize()
        {
            musicNoteMidiData = MidiNoteParser.ParseFromText(
                musicNoteCreationSetting.MidiContent.text
            );

            perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);
            perfectLineTag = perfectLineStorage.GetComponents<PerfectLineTagComponent>();
            perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>();

            var noteCount = musicNoteMidiData.TotalNotes;
            var durations = musicNoteMidiData.Durations;
            var positionIds = musicNoteMidiData.PositionIds;
            var timeAppears = musicNoteMidiData.TimeAppears;
            var minDuration = musicNoteMidiData.MinDuration;

            var componentsList = new List<object[]>(noteCount);

            for (int i = 0; i < noteCount; i++)
            {
                var musicNoteType =
                    durations[i] > minDuration ? MusicNoteType.LongNote : MusicNoteType.ShortNote;

                componentsList.Add(
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

            foreach (var components in componentsList)
            {
                World.CreateEntityWithComponents(Archetype.Registry.MusicNote, components);
            }

            musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);

            musicNoteTransforms = musicNoteStorage.GetComponents<TransformComponent>();
            musicNotes = musicNoteStorage.GetComponents<MusicNoteComponent>();
        }

        public void Update(float deltaTime)
        {
            if (
                lastPerfectLineTopLeftY != perfectLineCorners[0].TopLeft.y
                || lastPerfectLineTopLeftX != perfectLineCorners[0].TopLeft.x
            )
            {
                lastPerfectLineTopLeftY = perfectLineCorners[0].TopLeft.y;
                lastPerfectLineTopLeftX = perfectLineCorners[0].TopLeft.x;

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

        public void Cleanup()
        {
            //
        }

        private void CalculateMusicNoteData()
        {
            ref CornerComponent perfectLineCorner = ref perfectLineCorners[0];

            // Calculate lane width and half lane width
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            // Calculate Short and Long Note scale factors
            float shortNoteScaleYFactor = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.ShortNoteScaleYFactor,
                perfectLineTag[0].PerfectLineWidth / 4
            );
            float longNoteScaleYFactor = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.LongNoteScaleYFactor,
                perfectLineTag[0].PerfectLineWidth / 4
            );

            // Set all music note positions and sizes
            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                float spawnX =
                    perfectLineCorner.TopLeft.x
                    + (musicNoteMidiData.PositionIds[i] * laneWidth)
                    + halfLaneWidth;

                float spawnY =
                    perfectLineCorner.TopLeft.y
                    + (musicNoteMidiData.TimeAppears[i] * generalGameSetting.GameSpeed)
                    + (
                        musicNotes[i].musicNoteType == MusicNoteType.ShortNote
                            ? shortNoteScaleYFactor
                            : longNoteScaleYFactor
                    );

                musicNoteTransforms[i].Position = new Vector2(spawnX, spawnY);

                musicNoteTransforms[i].Size = new Vector2(
                    perfectLineTag[0].PerfectLineWidth / 4,
                    musicNotes[i].musicNoteType == MusicNoteType.ShortNote
                        ? shortNoteScaleYFactor
                        : longNoteScaleYFactor
                );
            }
        }

        private void CalculateMusicNoteDataPrecisely()
        {
            float[] noteSizes = PreciseNoteCalculator.CalculateNoteSizes(musicNoteMidiData, 1.4f);
            float[] positions = PreciseNoteCalculator.CalculateInitialPositions(
                musicNoteMidiData,
                perfectLineCorners[0].TopLeft.y,
                noteSizes
            );

            ref CornerComponent perfectLineCorner = ref perfectLineCorners[0];
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                float spawnX =
                    perfectLineCorner.TopLeft.x
                    + (musicNoteMidiData.PositionIds[i] * laneWidth)
                    + halfLaneWidth;

                musicNoteTransforms[i].Position = new Vector2(spawnX, positions[i]);
                musicNoteTransforms[i].Size = new Vector2(
                    perfectLineTag[0].PerfectLineWidth / 4,
                    noteSizes[i]
                );
            }
        }
    }
}

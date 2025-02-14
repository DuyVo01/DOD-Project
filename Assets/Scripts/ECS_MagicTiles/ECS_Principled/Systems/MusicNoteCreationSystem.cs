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

            for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
            {
                MusicNoteType musicNoteType = MusicNoteType.ShortNote;
                if (musicNoteMidiData.Durations[i] > musicNoteMidiData.MinDuration)
                {
                    musicNoteType = MusicNoteType.LongNote;
                }

                object[] components = new object[]
                {
                    new TransformComponent { },
                    new MusicNoteComponent
                    {
                        Duration = musicNoteMidiData.Durations[i],
                        PostionId = musicNoteMidiData.PositionIds[i],
                        TimeAppear = musicNoteMidiData.TimeAppears[i],
                        musicNoteType = musicNoteType,
                        musicNotePositionState = MusicNotePositionState.AbovePerfectLine,
                    },
                    new CornerComponent { },
                    new MusicNoteInteractionComponent { },
                    new MusicNoteFillerComponent { },
                    new ScoreStateComponent { HasBeenScored = false },
                };

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
                PrepareMusicNoteData();
            }
        }

        public void Cleanup()
        {
            //
        }

        private void PrepareMusicNoteData()
        {
            ref PerfectLineTagComponent PerfectLine = ref perfectLineTag[0];

            ref CornerComponent perfectLineCorner = ref perfectLineCorners[0];

            // Calculate lane width once
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                float spawnX =
                    perfectLineCorner.TopLeft.x
                    + (musicNoteMidiData.PositionIds[i] * laneWidth)
                    + halfLaneWidth;

                float spawnY =
                    perfectLineCorner.TopLeft.y
                    + (musicNoteMidiData.TimeAppears[i] * generalGameSetting.GameSpeed)
                    + musicNoteTransforms[i].Size.y / 2f;

                musicNoteTransforms[i].Position = new Vector2(spawnX, spawnY);

                float scaleX = PerfectLine.PerfectLineWidth / 4;

                float scaleY;

                if (musicNotes[i].musicNoteType == MusicNoteType.ShortNote)
                {
                    scaleY = MagicTileHelper.CalculateScaleY(
                        musicNoteCreationSetting.ShortNoteScaleYFactor,
                        scaleX
                    );
                }
                else
                {
                    scaleY = MagicTileHelper.CalculateScaleY(
                        musicNoteCreationSetting.LongNoteScaleYFactor,
                        scaleX,
                        musicNotes[i].Duration
                    );
                }

                musicNoteTransforms[i].Size = new Vector2(scaleX, scaleY);
            }
        }
    }
}

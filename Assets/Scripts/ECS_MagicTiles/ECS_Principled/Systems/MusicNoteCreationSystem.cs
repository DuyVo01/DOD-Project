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
            MusicNoteMidiData musicNoteMidiData = MidiNoteParser.ParseFromText(
                musicNoteCreationSetting.MidiContent.text
            );

            ArchetypeStorage perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            ref CornerComponent perfectLineCorners =
                ref perfectLineStorage.GetComponents<CornerComponent>()[0];

            // Calculate lane width once
            float totalWidth = perfectLineCorners.TopRight.x - perfectLineCorners.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

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

            ArchetypeStorage musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);

            TransformComponent[] transforms = musicNoteStorage.GetComponents<TransformComponent>();
            MusicNoteComponent[] musicNotes = musicNoteStorage.GetComponents<MusicNoteComponent>();

            for (int i = 0; i < musicNoteStorage.Count; i++)
            {
                float spawnX =
                    perfectLineCorners.TopLeft.x
                    + (musicNoteMidiData.PositionIds[i] * laneWidth)
                    + halfLaneWidth;

                float spawnY =
                    perfectLineCorners.TopLeft.y
                    + (musicNoteMidiData.TimeAppears[i] * generalGameSetting.GameSpeed)
                    + transforms[i].Size.y / 2f;

                transforms[i].Posision = new Vector2(spawnX, spawnY);

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

                transforms[i].Size = new Vector2(scaleX, scaleY);
            }
        }

        public void Update(float deltaTime)
        {
            //
        }

        public void Cleanup()
        {
            //
        }
    }
}

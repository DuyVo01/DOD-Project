using System;
using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ScoringSystem : IGameSystem
    {
        private const int PERFECT_SCORE = 100;
        private const int GREAT_SCORE = 50;

        // How close to perfect line needed for perfect score (in units)
        private const float PERFECT_THRESHOLD = 0.1f;

        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.Ingame;

        ArchetypeStorage noteStorage;
        ArchetypeStorage gameScoreStorage;
        ArchetypeStorage perfectLineStorage;

        MusicNoteInteractionComponent[] musicNoteInteractionComponents;
        TransformComponent[] musicNoteTransformComponents;
        ScoreStateComponent[] musicScoreStateComponents;
        MusicNoteComponent[] musicNoteComponents;

        BoolEventChannel scoreSignalEffectChannel;

        private GameScoreSyncTool gameScoreSyncTool;

        public ScoringSystem(GlobalPoint globalPoint)
        {
            this.scoreSignalEffectChannel = globalPoint.OnScoreHitChannel;
            this.gameScoreSyncTool = globalPoint.gameScoreSyncTool;
        }

        public void Cleanup()
        {
            //
        }

        public void Initialize()
        {
            noteStorage = World.GetStorage(Archetype.Registry.MusicNote);
            gameScoreStorage = World.GetStorage(Archetype.Registry.GameScore);
            perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);

            musicNoteComponents = noteStorage.GetComponents<MusicNoteComponent>();
            musicNoteInteractionComponents =
                noteStorage.GetComponents<MusicNoteInteractionComponent>();
            musicNoteTransformComponents = noteStorage.GetComponents<TransformComponent>();
            musicScoreStateComponents = noteStorage.GetComponents<ScoreStateComponent>();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            var perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>()[0];
            ref var gameScore = ref gameScoreStorage.GetComponents<ScoreComponent>()[0];

            //process note
            for (int i = 0; i < noteStorage.Count; i++)
            {
                // Only process notes that were just hit and haven't been scored
                if (musicScoreStateComponents[i].HasBeenScored)
                {
                    continue;
                }

                if (musicNoteComponents[i].musicNoteType == MusicNoteType.LongNote)
                {
                    if (
                        musicNoteInteractionComponents[i].State == MusicNoteInteractiveState.Pressed
                    )
                    {
                        ProcessNoteScore(
                            musicNoteTransformComponents[i],
                            perfectLineCorners,
                            ref gameScore,
                            ref musicScoreStateComponents[i]
                        );
                    }
                }
                else
                {
                    if (
                        musicNoteInteractionComponents[i].State == MusicNoteInteractiveState.Pressed
                        || musicNoteInteractionComponents[i].State
                            == MusicNoteInteractiveState.Completed
                    )
                    {
                        ProcessNoteScore(
                            musicNoteTransformComponents[i],
                            perfectLineCorners,
                            ref gameScore,
                            ref musicScoreStateComponents[i]
                        );
                    }
                }
            }
        }

        private void ProcessNoteScore(
            TransformComponent musicTransform,
            CornerComponent perfectLineCorners,
            ref ScoreComponent gameScore,
            ref ScoreStateComponent scoreStateComponent
        )
        {
            float distanceFromPerfect = Mathf.Abs(
                musicTransform.Position.y - perfectLineCorners.TopLeft.y
            );

            int scoreToAdd;

            if (distanceFromPerfect <= PERFECT_THRESHOLD)
            {
                scoreToAdd = PERFECT_SCORE;
                scoreSignalEffectChannel.RaiseEvent(true);
            }
            else
            {
                scoreToAdd = GREAT_SCORE;
                scoreSignalEffectChannel.RaiseEvent(false);
            }

            gameScore.TotalScore += scoreToAdd;
            scoreStateComponent.HasBeenScored = true;

            gameScoreSyncTool.SyncGameScore(gameScore);
        }
    }
}

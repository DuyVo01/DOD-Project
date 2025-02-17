using System.Collections;
using System.Collections.Generic;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class InputCollisionSystem : IGameSystem
    {
        private const string LOG_PREFIX = "[Input Collision] ";
        private const int MAX_INPUTS = 2;

        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.Ingame;

        private readonly GeneralGameSetting generalGameSetting;
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;

        ArchetypeStorage inputStorage;
        ArchetypeStorage musicNoteStorage;

        InputStateComponent[] inputStates;
        CornerComponent[] musicNoteCorners;
        MusicNoteInteractionComponent[] musicNoteInteractions;
        MusicNoteFillerComponent[] musicNoteFillers;
        MusicNoteComponent[] musicNotes;

        private readonly MusicNoteViewSyncTool musicNoteViewSyncTool;

        public InputCollisionSystem(GlobalPoint globalPoint)
        {
            this.generalGameSetting = globalPoint.generalGameSetting;
            musicNoteViewSyncTool = globalPoint.musicNoteViewSyncTool;
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Initialize()
        {
            musicNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);
            inputStorage = World.GetStorage(Archetype.Registry.Input);

            inputStates = inputStorage.GetComponents<InputStateComponent>();
            musicNoteCorners = musicNoteStorage.GetComponents<CornerComponent>();
            musicNoteInteractions = musicNoteStorage.GetComponents<MusicNoteInteractionComponent>();
            musicNoteFillers = musicNoteStorage.GetComponents<MusicNoteFillerComponent>();
            musicNotes = musicNoteStorage.GetComponents<MusicNoteComponent>();
        }

        public void Update(float deltaTime)
        {
            for (int inputIdx = 0; inputIdx < MAX_INPUTS; inputIdx++)
            {
                if (!inputStates[inputIdx].IsActive)
                    continue;

                for (int noteIdx = 0; noteIdx < musicNoteStorage.Count; noteIdx++)
                {
                    ProcessNoteCollision(
                        inputStates[inputIdx],
                        ref musicNoteCorners[noteIdx],
                        ref musicNoteInteractions[noteIdx],
                        ref musicNoteFillers[noteIdx],
                        musicNotes[noteIdx]
                    );
                }
            }

            musicNoteViewSyncTool.SyncNoteState(
                musicNoteInteractions,
                musicNoteFillers,
                musicNotes
            );
        }

        private void ProcessNoteCollision(
            InputStateComponent input,
            ref CornerComponent corners,
            ref MusicNoteInteractionComponent interaction,
            ref MusicNoteFillerComponent filler,
            MusicNoteComponent note
        )
        {
            // Skip completed notes
            if (interaction.State == MusicNoteInteractiveState.Completed)
                return;

            bool isInsideNote = IsPointInNote(
                input.Position,
                corners.TopLeft,
                corners.TopRight,
                corners.BottomLeft,
                corners.BottomRight
            );

            if (!isInsideNote)
                return;

            ProcessNoteInteraction(input, ref interaction, ref corners, ref filler, note);
        }

        private void ProcessNoteInteraction(
            InputStateComponent input,
            ref MusicNoteInteractionComponent interaction,
            ref CornerComponent corners,
            ref MusicNoteFillerComponent filler,
            MusicNoteComponent note
        )
        {
            switch (input.State)
            {
                case InputState.JustPressed:
                    if (interaction.State == MusicNoteInteractiveState.Normal)
                    {
                        if (note.musicNoteType == MusicNoteType.ShortNote)
                        {
                            CompleteNote(ref interaction);
                        }
                        else
                        {
                            StartLongNote(input, ref interaction, ref corners, ref filler);
                        }
                    }
                    break;

                case InputState.Held:
                    if (note.musicNoteType == MusicNoteType.LongNote)
                    {
                        if (interaction.State == MusicNoteInteractiveState.Pressed)
                        {
                            interaction.State = MusicNoteInteractiveState.Hold;
                        }
                        else if (interaction.State == MusicNoteInteractiveState.Hold)
                        {
                            UpdateLongNoteFill(ref interaction, ref corners, ref filler);
                        }
                    }
                    break;

                case InputState.JustReleased:
                    if (
                        note.musicNoteType == MusicNoteType.LongNote
                        && (
                            interaction.State == MusicNoteInteractiveState.Pressed
                            || interaction.State == MusicNoteInteractiveState.Hold
                        )
                    )
                    {
                        CompleteNote(ref interaction);
                    }
                    break;
            }
        }

        private void CompleteNote(ref MusicNoteInteractionComponent interaction)
        {
            interaction.State = MusicNoteInteractiveState.Completed;
            Debug.Log($"{LOG_PREFIX} Note completed");
        }

        private void StartLongNote(
            InputStateComponent input,
            ref MusicNoteInteractionComponent interaction,
            ref CornerComponent corners,
            ref MusicNoteFillerComponent filler
        )
        {
            interaction.State = MusicNoteInteractiveState.Pressed;
            filler.IsVisible = true;

            float sizeOfNote = corners.TopLeft.y - corners.BottomLeft.y;
            float fromTouchPositionToLowerOfNote = input.Position.y - corners.BottomLeft.y;
            float touchPercent = fromTouchPositionToLowerOfNote / sizeOfNote;
            filler.FillPercent = touchPercent + 0.1f;

            Debug.Log($"{LOG_PREFIX} Long note pressed");
        }

        private void UpdateLongNoteFill(
            ref MusicNoteInteractionComponent interaction,
            ref CornerComponent corners,
            ref MusicNoteFillerComponent filler
        )
        {
            float gameSpeed = generalGameSetting.GameSpeed;
            if (musicNoteCreationSetting.UsePreciseNoteCalculation)
            {
                gameSpeed = generalGameSetting.PreciseGameSpeed;
            }
            float noteLength = corners.TopLeft.y - corners.BottomLeft.y;
            float fillSpeed = gameSpeed / noteLength;

            float nextFillPercent = filler.FillPercent + (fillSpeed * Time.deltaTime);
            nextFillPercent = Mathf.Min(nextFillPercent, 1f);

            filler.FillPercent = nextFillPercent;

            if (nextFillPercent >= 1f)
            {
                CompleteNote(ref interaction);
            }
        }

        private static bool IsPointInNote(
            Vector2 point,
            Vector2 topLeft,
            Vector2 topRight,
            Vector2 bottomLeft,
            Vector2 bottomRight
        )
        {
            int wn = 0; // Winding number

            Vector2[] vertices = { topLeft, topRight, bottomRight, bottomLeft };

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector2 current = vertices[i];
                Vector2 next = vertices[(i + 1) % vertices.Length];

                if (current.y <= point.y)
                {
                    if (next.y > point.y && IsLeftOf(current, next, point) > 0)
                        wn++;
                }
                else
                {
                    if (next.y <= point.y && IsLeftOf(current, next, point) < 0)
                        wn--;
                }
            }

            return wn != 0;
        }

        private static float IsLeftOf(Vector2 a, Vector2 b, Vector2 point)
        {
            return (b.x - a.x) * (point.y - a.y) - (point.x - a.x) * (b.y - a.y);
        }

        public void Cleanup() { }
    }
}

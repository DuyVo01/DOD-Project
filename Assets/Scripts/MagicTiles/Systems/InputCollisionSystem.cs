using UnityEngine;

public struct InputCollisionSystem : IGameSystem
{
    private const string LOG_PREFIX = "[Input Collision] ";

    public void ProcessCollisions(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData,
        ref MusicNoteFillerData musicNoteFillerData
    )
    {
        ref var inputData = ref SingletonComponentRepository.GetComponent<InputDataComponent>(
            SingletonComponentType.Input
        );
        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        // Process each active input
        for (int inputIdx = 0; inputIdx < InputDataComponent.MAX_INPUTS; inputIdx++)
        {
            if (!inputData.isActives.Get(inputIdx))
                continue;

            var inputState = inputData.inputStates.Get(inputIdx);
            Vector2 inputPosition = inputState.Position;

            // Skip notes not in playable zone
            if (
                musicNoteStateData.positionStates.Get(entityId)
                != MusicNotePositionState.InlineWithPerfectLine
            )
                continue;

            // Skip completed notes
            if (
                musicNoteStateData.interactiveStates.Get(entityId)
                == MusicNoteInteractiveState.Completed
            )
                continue;

            bool isInsideNote = IsPointInNote(
                inputPosition,
                musicNoteTransformData.TopLeft.Get(entityId),
                musicNoteTransformData.TopRight.Get(entityId),
                musicNoteTransformData.BottomLeft.Get(entityId),
                musicNoteTransformData.BottomRight.Get(entityId)
            );

            if (!isInsideNote)
            {
                continue;
            }

            ProcessNoteInteraction(
                entityId,
                inputState,
                ref musicNoteStateData,
                ref musicNoteTransformData,
                ref musicNoteFillerData
            );
        }
    }

    private void ProcessNoteInteraction(
        int entityId,
        InputStateData inputState,
        ref MusicNoteStateData stateData,
        ref MusicNoteTransformData transformData,
        ref MusicNoteFillerData musicNoteFillerData
    )
    {
        var currentInteractiveState = stateData.interactiveStates.Get(entityId);
        var noteType = stateData.noteTypes.Get(entityId);

        switch (inputState.State)
        {
            case InputState.JustPressed:
                if (currentInteractiveState == MusicNoteInteractiveState.Normal)
                {
                    // For short notes, immediately complete after press
                    if (noteType == MusicNoteType.ShortNote)
                    {
                        stateData.interactiveStates.Set(
                            entityId,
                            MusicNoteInteractiveState.Completed
                        );
                        break;
                    }
                    //Long note process
                    stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Pressed);
                    musicNoteFillerData.IsVisibles.Set(entityId, true);

                    Debug.Log($"{LOG_PREFIX} Note {entityId} pressed");
                }
                break;

            case InputState.Held:
                if (noteType == MusicNoteType.LongNote)
                {
                    if (currentInteractiveState == MusicNoteInteractiveState.Pressed)
                    {
                        stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Hold);
                        Debug.Log($"{LOG_PREFIX} Long note {entityId} entering hold state");
                    }
                    else if (currentInteractiveState == MusicNoteInteractiveState.Hold)
                    {
                        // Temporary completion condition: Check if input is above note's top edge
                        float noteTopY = transformData.TopLeft.Get(entityId).y;

                        if (inputState.Position.y > noteTopY)
                        {
                            stateData.interactiveStates.Set(
                                entityId,
                                MusicNoteInteractiveState.Completed
                            );
                            Debug.Log($"{LOG_PREFIX} Long note {entityId} completed");
                        }
                    }
                }
                break;

            case InputState.JustReleased:
                if (
                    noteType == MusicNoteType.LongNote
                    && (
                        currentInteractiveState == MusicNoteInteractiveState.Pressed
                        || currentInteractiveState == MusicNoteInteractiveState.Hold
                    )
                )
                {
                    stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Completed);
                    Debug.Log($"{LOG_PREFIX} Long note {entityId} released and completed");
                }
                break;
        }
    }

    private bool IsPointInNote(
        Vector2 point,
        Vector2 topLeft,
        Vector2 topRight,
        Vector2 bottomLeft,
        Vector2 bottomRight
    )
    {
        int wn = 0; // Winding number

        // Using winding number algorithm for accurate polygon containment
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

    private float IsLeftOf(Vector2 a, Vector2 b, Vector2 point)
    {
        return (b.x - a.x) * (point.y - a.y) - (point.x - a.x) * (b.y - a.y);
    }
}

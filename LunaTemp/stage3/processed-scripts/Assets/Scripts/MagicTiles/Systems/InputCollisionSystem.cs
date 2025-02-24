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

        // Process each active input
        for (int inputIdx = 0; inputIdx < InputDataComponent.MAX_INPUTS; inputIdx++)
        {
            if (!inputData.isActives.Get(inputIdx))
                continue;

            var inputState = inputData.inputStates.Get(inputIdx);
            Vector2 inputPosition = inputState.Position;

            // // Skip notes not in playable zone
            // if (
            //     musicNoteStateData.positionStates.Get(entityId)
            //     != MusicNotePositionState.InlineWithPerfectLine
            // )
            //     continue;

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

    private static void ProcessNoteInteraction(
        int entityId,
[Bridge.Ref]         InputStateData inputState,
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
                    if (noteType == MusicNoteType.ShortNote)
                    {
                        CompleteNote(entityId, ref stateData);
                    }
                    else
                    {
                        StartLongNote(
                            entityId,
                            inputState,
                            ref stateData,
                            ref transformData,
                            ref musicNoteFillerData
                        );
                    }
                }
                break;

            case InputState.Held:
                if (noteType == MusicNoteType.LongNote)
                {
                    if (currentInteractiveState == MusicNoteInteractiveState.Pressed)
                    {
                        EnterHoldState(entityId, ref stateData);
                    }
                    else if (currentInteractiveState == MusicNoteInteractiveState.Hold)
                    {
                        UpdateLongNoteFill(
                            entityId,
                            ref stateData,
                            ref transformData,
                            ref musicNoteFillerData
                        );
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
                    CompleteNote(entityId, ref stateData);
                }
                break;
        }
    }

    private static void CompleteNote(int entityId, ref MusicNoteStateData stateData)
    {
        stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Completed);
        Debug.Log($"{LOG_PREFIX} Note {entityId} completed");
    }

    private static void StartLongNote(
        int entityId,
[Bridge.Ref]         InputStateData inputState,
        ref MusicNoteStateData stateData,
        ref MusicNoteTransformData transformData,
        ref MusicNoteFillerData musicNoteFillerData
    )
    {
        stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Pressed);
        musicNoteFillerData.IsVisibles.Set(entityId, true);

        float sizeOfNote =
            transformData.TopLeft.Get(entityId).y - transformData.BottomLeft.Get(entityId).y;
        float fromTouchPositionToLowerOfNote =
            inputState.Position.y - transformData.BottomLeft.Get(entityId).y;
        float touchPercent = fromTouchPositionToLowerOfNote / sizeOfNote;
        musicNoteFillerData.FillPercent.Set(entityId, touchPercent + 0.1f);

        Debug.Log($"{LOG_PREFIX} Note {entityId} pressed");
    }

    private static void EnterHoldState(int entityId, ref MusicNoteStateData stateData)
    {
        stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Hold);
        Debug.Log($"{LOG_PREFIX} Long note {entityId} entering hold state");
    }

    private static void UpdateLongNoteFill(
        int entityId,
        ref MusicNoteStateData stateData,
        ref MusicNoteTransformData transformData,
        ref MusicNoteFillerData musicNoteFillerData
    )
    {
        float noteLength =
            transformData.TopLeft.Get(entityId).y - transformData.BottomLeft.Get(entityId).y;
        float gameSpeed = GlobalGameSetting.Instance.generalSetting.gameSpeed;
        float fillSpeed = gameSpeed / noteLength;

        float currentFillPercent = musicNoteFillerData.FillPercent.Get(entityId);
        float fillAmount = fillSpeed * Time.deltaTime;
        float nextFillPercent = currentFillPercent + fillAmount;
        nextFillPercent = Mathf.Min(nextFillPercent, 1f);

        musicNoteFillerData.FillPercent.Set(entityId, nextFillPercent);

        if (nextFillPercent >= 1f)
        {
            CompleteNote(entityId, ref stateData);
        }
    }

    private static bool IsPointInNote(
[Bridge.Ref]         Vector2 point,
[Bridge.Ref]         Vector2 topLeft,
[Bridge.Ref]         Vector2 topRight,
[Bridge.Ref]         Vector2 bottomLeft,
[Bridge.Ref]         Vector2 bottomRight
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

    private static float IsLeftOf([Bridge.Ref] Vector2 a, [Bridge.Ref] Vector2 b, [Bridge.Ref] Vector2 point)
    {
        return (b.x - a.x) * (point.y - a.y) - (point.x - a.x) * (b.y - a.y);
    }
}

using UnityEngine;

public struct InputCollisionSystem : IGameSystem
{
    private const string LOG_PREFIX = "[Input Collision] ";

    public void ProcessCollisions()
    {
        ref var inputData = ref SingletonComponentRepository.GetComponent<InputDataComponent>(
            SingletonComponentType.Input
        );
        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var transformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );
        ref var stateData = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        // Process each active input
        for (int inputIdx = 0; inputIdx < InputDataComponent.MAX_INPUTS; inputIdx++)
        {
            if (!inputData.isActives.Get(inputIdx))
                continue;

            var inputState = inputData.inputStates.Get(inputIdx);

            if (inputState.State != InputState.JustPressed)
                continue;

            Debug.Log($"{LOG_PREFIX} Processing input at position: {inputState.Position}");

            // Check collision with each note
            CheckCollisionsForInput(
                inputState.Position,
                ref noteEntityGroup,
                ref transformData,
                ref stateData
            );
        }
    }

    private void CheckCollisionsForInput(
        Vector2 inputPosition,
        ref EntityGroup<MusicNoteComponentType> entityGroup,
        ref MusicNoteTransformData transformData,
        ref MusicNoteStateData stateData
    )
    {
        int playableNotesCount = 0;

        for (int entityId = 0; entityId < entityGroup.EntityCount; entityId++)
        {
            // Only check notes that are in the playable zone
            if (
                stateData.positionStates.Get(entityId)
                != MusicNotePositionState.InlineWithPerfectLine
            )
                continue;

            playableNotesCount++;

            // Get tile boundaries
            Vector2 topLeft = transformData.TopLeft.Get(entityId);
            Vector2 topRight = transformData.TopRight.Get(entityId);
            Vector2 bottomLeft = transformData.BottomLeft.Get(entityId);
            Vector2 bottomRight = transformData.BottomRight.Get(entityId);

            // Log detailed boundary information
            Debug.Log(
                $"{LOG_PREFIX} Note {entityId} boundaries:"
                    + $"\nTopLeft: {topLeft}"
                    + $"\nTopRight: {topRight}"
                    + $"\nBottomLeft: {bottomLeft}"
                    + $"\nBottomRight: {bottomRight}"
                    + $"\nInput Position: {inputPosition}"
            );

            // Check if point is inside the tile using winding number algorithm
            if (IsPointInPolygon(inputPosition, topLeft, topRight, bottomRight, bottomLeft))
            {
                Debug.Log($"{LOG_PREFIX} Hit detected on note {entityId}!");
                stateData.interactiveStates.Set(entityId, MusicNoteInteractiveState.Pressed);
            }
        }

        Debug.Log($"{LOG_PREFIX} Checked {playableNotesCount} playable notes for collisions");
    }

    private bool IsPointInPolygon(Vector2 point, params Vector2[] vertices)
    {
        // Using the winding number algorithm for more accurate polygon containment
        int wn = 0; // Winding number

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

        bool isInside = wn != 0;
        Debug.Log(
            $"{LOG_PREFIX} Point {point} is {(isInside ? "inside" : "outside")} polygon. Winding number: {wn}"
        );
        return isInside;
    }

    private float IsLeftOf(Vector2 a, Vector2 b, Vector2 point)
    {
        return ((b.x - a.x) * (point.y - a.y) - (point.x - a.x) * (b.y - a.y));
    }
}

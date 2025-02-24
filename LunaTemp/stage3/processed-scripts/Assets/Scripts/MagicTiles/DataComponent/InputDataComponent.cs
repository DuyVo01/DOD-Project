using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputStateData
{
    public Vector2 Position;
    public Vector2 PreviousPosition;
    public InputState State;
    public int FrameCount;
}

public enum InputState
{
    None,
    JustPressed,
    Held,
    JustReleased,
}

public struct InputDataComponent : IDataComponent
{
    public const int MAX_INPUTS = 2;

    public ChunkArray<InputStateData> inputStates;
    public ChunkArray<bool> isActives;
    public int activeInputCount;

    public InputDataComponent(int capacity)
    {
        inputStates = new ChunkArray<InputStateData>(capacity);
        isActives = new ChunkArray<bool>(capacity);
        activeInputCount = 0;

        for (int i = 0; i < MAX_INPUTS; i++)
        {
            inputStates.Add(
                new InputStateData
                {
                    Position = Vector2.zero,
                    PreviousPosition = Vector2.zero,
                    State = InputState.None,
                    FrameCount = 0,
                }
            );
            isActives.Add(false);
        }
    }
}

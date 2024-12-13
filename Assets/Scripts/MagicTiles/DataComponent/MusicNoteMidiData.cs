using UnityEngine;

public struct MusicNoteMidiData : IDataComponent
{
    // Core data arrays
    public int[] Ids;
    public int[] NoteNumbers;
    public int[] PositionIds;
    public float[] TimeAppears;
    public float[] Timespans;
    public float[] Durations;
    public float[] Velocities;

    public float[] PosX;
    public float[] PosY;

    // Metadata
    public int TotalNotes;
    public float MinDuration;

    public MusicNoteMidiData(int capacity)
    {
        // Preallocate arrays with specified capacity
        Ids = new int[capacity];
        NoteNumbers = new int[capacity];
        TimeAppears = new float[capacity];
        Timespans = new float[capacity];
        Durations = new float[capacity];
        Velocities = new float[capacity];
        PositionIds = new int[capacity];
        PosX = new float[capacity];
        PosY = new float[capacity];

        TotalNotes = 0;
        MinDuration = float.MaxValue;
    }

    public void SetPosition(int index, float x, float y)
    {
        PosX[index] = x;
        PosY[index] = y;
    }

    public void SetPositionX(int index, float value)
    {
        PosX[index] = value;
    }

    public void SetPositionY(int index, float value)
    {
        PosY[index] = value;
    }

    public void GetPosition(int index, out float x, out float y)
    {
        x = PosX[index];
        y = PosY[index];
    }
}

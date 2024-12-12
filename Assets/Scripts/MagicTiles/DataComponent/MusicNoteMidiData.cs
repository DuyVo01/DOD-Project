using UnityEngine;

public struct MusicNoteMidiData
{
    // Core data arrays
    public int[] Ids;
    public int[] NoteNumbers;
    public float[] TimeAppears;
    public float[] Timespans;
    public float[] Durations;
    public float[] Velocities;
    public int[] PositionIds;
    public Vector2[] Positions;

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
        Positions = new Vector2[capacity];
        TotalNotes = 0;
        MinDuration = float.MaxValue;
    }
}

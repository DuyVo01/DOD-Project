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

        TotalNotes = 0;
        MinDuration = float.MaxValue;
    }
}

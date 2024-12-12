using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public struct MusicNoteMidiData
{
    public int[] ids;

    // public int[] notes_num { get; set; }
    public int[] pos_ids;

    // public int[] mood_changes { get; set; }
    public float[] time_appears;

    // public float[] timespans { get; set; }
    // public float[] durations { get; set; }
    // public float[] velocities { get; set; }
    public Vector2[] positions;

    // public float min_duration ;
    // public int raw_total_notes ;
    // public int total_notes ;

    public int count;

    public MusicNoteMidiData(int capacity)
    {
        ids = new int[capacity];
        pos_ids = new int[capacity];
        time_appears = new float[capacity];
        positions = new Vector2[capacity];
        count = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote_DataComponent
{
    public int[] ids { get; set; }
    public int raw_total_notes { get; set; }
    public int total_notes { get; set; }
    public int[] notes_num { get; set; }
    public float[] time_appears { get; set; }
    public float[] timespans { get; set; }
    public float[] durations { get; set; }
    public float min_duration { get; set; }
    public float[] velocities { get; set; }
    public int[] pos_ids { get; set; }
    public int[] mood_changes { get; set; }
}

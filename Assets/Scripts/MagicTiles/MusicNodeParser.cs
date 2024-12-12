using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class MusicNodeParser
{
    public static List<Node> ParseNodeFromFile(string filePath)
    {
        List<Node> nodes = new List<Node>();
        string content = File.ReadAllText(filePath);

        string[] rawEntries = content.Split(
            new[] { "id:" },
            System.StringSplitOptions.RemoveEmptyEntries
        );

        Node node = new Node();
        string[] properties;

        string[] keyValue;
        string key;
        string value;
        foreach (var entry in rawEntries) { }

        return nodes;
    }
}

public class Node
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

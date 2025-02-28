using System;
using System.Collections.Generic;
using UnityEngine;

public static class MidiNoteParser
{
    public static MusicNoteMidiData ParseFromText(string content)
    {
        if (string.IsNullOrEmpty(content))
            throw new MidiParseException("MIDI content cannot be empty");

        var entries = content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        if (entries.Length == 0)
            throw new MidiParseException("No valid MIDI entries found");

        // Split into note entries
        var data = new MusicNoteMidiData(entries.Length);

        for (int i = 0; i < entries.Length; i++)
        {
            try
            {
                ParseEntry(entries[i], i, ref data);
                data.TotalNotes++;
            }
            catch (Exception ex)
            {
                throw new MidiParseException($"Error parsing entry {i}: {ex.Message}");
            }
        }

        ValidateData(ref data);

        return data;
    }

    private static void ParseEntry(string entry, int index, ref MusicNoteMidiData data)
    {
        var properties = entry.Split('-');
        var requiredFields = new HashSet<string> { "id", "n", "ta", "ts", "d", "v", "pid" };
        var parsedFields = new HashSet<string>();

        foreach (var prop in properties)
        {
            var kv = prop.Split(':');
            if (kv.Length != 2)
                throw new MidiParseException($"Invalid property format: {prop}");

            if (!ParseProperty(kv[0], kv[1], index, ref data))
                throw new MidiParseException($"Failed to parse property: {kv[0]}");

            parsedFields.Add(kv[0]);
        }

        // Check for missing required fields
        requiredFields.ExceptWith(parsedFields);
        if (requiredFields.Count > 0)
            throw new MidiParseException(
                $"Missing required fields: {string.Join(", ", requiredFields)}"
            );
    }

    private static bool ParseProperty(
        string key,
        string value,
        int index,
        ref MusicNoteMidiData data
    )
    {
        try
        {
            switch (key)
            {
                case "id":
                    data.Ids[index] = int.Parse(value);
                    break;
                case "n":
                    data.NoteNumbers[index] = int.Parse(value);
                    break;
                case "ta":
                    data.TimeAppears[index] = float.Parse(value);
                    break;
                case "ts":
                    data.Timespans[index] = float.Parse(value);
                    break;
                case "d":
                    float duration = float.Parse(value);
                    data.Durations[index] = duration;
                    data.MinDuration = Mathf.Min(data.MinDuration, duration);
                    break;
                case "v":
                    data.Velocities[index] = float.Parse(value);
                    break;
                case "pid":
                    data.PositionIds[index] = int.Parse(value);
                    break;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void ValidateData(ref MusicNoteMidiData data)
    {
        if (data.MinDuration <= 0)
            throw new MidiParseException("Invalid minimum duration");

        if (data.TotalNotes <= 0)
            throw new MidiParseException("No valid notes parsed");
    }
}

public class MidiParseException : Exception
{
    public MidiParseException(string message)
        : base(message) { }
}

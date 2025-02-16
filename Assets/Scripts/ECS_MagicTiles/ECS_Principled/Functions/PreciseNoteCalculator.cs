using System;
using UnityEngine;

public static class PreciseNoteCalculator
{
    /// <summary>
    /// Calculates the total song duration, in seconds, by summing the last
    /// note's appear time and duration.
    /// </summary>
    /// <param name="musicNoteMidiData">The parsed MIDI data to calculate the total duration from.</param>
    /// <returns>The total song duration, in seconds.</returns>
    ///
    public static float CalculateTotalSongDuration(MusicNoteMidiData musicNoteMidiData)
    {
        return musicNoteMidiData.TimeAppears[musicNoteMidiData.TotalNotes - 1]
            + musicNoteMidiData.Durations[musicNoteMidiData.TotalNotes - 1];
    }

    /// <summary>
    /// Finds the smallest note duration in the given MIDI data.
    /// </summary>
    /// <param name="musicNoteMidiData">The parsed MIDI data to find the smallest note duration from.</param>
    /// <param name="decimalPlaces">The number of decimal places to round the smallest duration to.</param>
    /// <returns>The smallest note duration, rounded to the specified number of decimal places.</returns>
    public static float FindSmallestNoteDuration(
        MusicNoteMidiData musicNoteMidiData,
        int decimalPlaces = 2
    )
    {
        if (musicNoteMidiData.TotalNotes == 0)
            return 0f;

        float smallestDuration = musicNoteMidiData.Durations[0];
        for (int i = 1; i < musicNoteMidiData.TotalNotes; i++)
        {
            smallestDuration = Mathf.Min(smallestDuration, musicNoteMidiData.Durations[i]);
        }

        return (float)Math.Round(smallestDuration, decimalPlaces);
    }

    /// <summary>
    /// Calculates the size of each note based on its duration compared to the smallest note duration.
    /// </summary>
    /// <param name="musicNoteMidiData">The parsed MIDI data to calculate the note sizes from.</param>
    /// <param name="baseSize">The base size of each note, to be scaled according to its duration.</param>
    /// <returns>An array of note sizes, where each size is relative to the smallest note duration.</returns>
    ///
    public static float[] CalculateNoteSizes(
        MusicNoteMidiData musicNoteMidiData,
        float baseSize = 2f
    )
    {
        float smallestDuration = FindSmallestNoteDuration(musicNoteMidiData, 2);
        float[] noteSizes = new float[musicNoteMidiData.TotalNotes];

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            noteSizes[i] = baseSize * (musicNoteMidiData.Durations[i] / smallestDuration);
        }

        return noteSizes;
    }

    public static float[] CalculateInitialPositions(
        MusicNoteMidiData midiData,
        float perfectLineY,
        float[] noteSizes
    )
    {
        float[] positions = new float[noteSizes.Length];
        if (noteSizes.Length == 0)
            return positions;

        // Position first note considering its half size
        positions[0] = perfectLineY + (noteSizes[0] * 0.5f);

        // Position subsequent notes sequentially
        for (int i = 1; i < noteSizes.Length; i++)
        {
            // Start from previous note's center
            float pos = positions[i - 1];
            // Add previous note's half size
            pos += noteSizes[i - 1] * 0.5f;
            // Add current note's half size
            pos += noteSizes[i] * 0.5f;

            positions[i] = pos;
        }

        return positions;
    }

    // Calculate road length from last note position and size
    public static float CalculateRoadLength(float[] positions, float[] sizes)
    {
        if (positions.Length == 0)
            return 0f;

        int lastIndex = positions.Length - 1;
        // Last note center position plus its half size gives total road length
        return positions[lastIndex] + (sizes[lastIndex] * 0.5f);
    }

    // Calculate velocity based on road length and total time
    public static float CalculateRequiredVelocity(float totalTime, float roadLength)
    {
        if (totalTime <= 0f)
            return 0f;
        return roadLength / totalTime;
    }
}

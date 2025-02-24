using System;
using UnityEngine;

namespace ECS_MagicTile
{
    public static class PreciseNoteCalculator
    {
        /// <summary>
        /// Calculates the total song duration, in seconds, by summing the last
        /// note's appear time and duration.
        /// </summary>
        /// <param name="musicNoteMidiData">The parsed MIDI data to calculate the total duration from.</param>
        /// <returns>The total song duration, in seconds.</returns>
        ///
        public static float CalculateTotalSongDuration([Bridge.Ref] MusicNoteMidiData musicNoteMidiData)
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
[Bridge.Ref]             MusicNoteMidiData musicNoteMidiData,
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
[Bridge.Ref]             MusicNoteMidiData musicNoteMidiData,
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
[Bridge.Ref]             MusicNoteMidiData midiData,
            float perfectLineY,
            float[] noteSizes,
            float baseSize = 2f
        )
        {
            float[] positions = new float[noteSizes.Length];
            if (noteSizes.Length == 0)
                return positions;

            // Position first note - add half its size to center point
            positions[0] = perfectLineY + (noteSizes[0] * 0.5f);
            float smallestDuration = FindSmallestNoteDuration(midiData, 2);

            // Position subsequent notes with proper gaps
            for (int i = 1; i < noteSizes.Length; i++)
            {
                // Start from previous note's center
                float pos = positions[i - 1];

                // Add half size of previous note (to get to its top)
                pos += noteSizes[i - 1] * 0.5f;

                // Calculate and add remaining gap space
                float remainingGap = midiData.Timespans[i] - midiData.Durations[i - 1];
                if (remainingGap > 0)
                {
                    // Convert time gap to space using velocity
                    float gapSpace = baseSize * (remainingGap / smallestDuration);
                    pos += gapSpace;
                }

                // Add half size of current note (to get to its center)
                pos += noteSizes[i] * 0.5f;

                positions[i] = pos;
            }

            return positions;
        }

        // Calculate road length from last note position and size
        public static float CalculateRoadLength(float[] noteSizes, [Bridge.Ref] MusicNoteMidiData midiData)
        {
            float totalLength = noteSizes[0]; // First note's size

            // Add subsequent notes' sizes and their gaps
            for (int i = 1; i < noteSizes.Length; i++)
            {
                // Add remaining gap space
                float remainingGap = midiData.Timespans[i] - midiData.Durations[i - 1];
                if (remainingGap > 0)
                {
                    // Estimate gap space relative to note sizes
                    float gapScale = remainingGap / midiData.Durations[i];
                    totalLength += noteSizes[i] * gapScale;
                }

                // Add current note's size
                totalLength += noteSizes[i];
            }

            return totalLength;
        }

        // Calculate velocity based on road length and total time
        public static float CalculateRequiredVelocity(float totalTime, float roadLength)
        {
            if (totalTime <= 0f)
                return 0f;
            return roadLength / totalTime;
        }
    }
}

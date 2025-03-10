using System;
using ECS_MagicTile.Components;
using UnityEngine;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    /// <summary>
    /// A cache-optimized version of the moving note system
    /// Demonstrates how to use the block-based processing for better performance
    /// </summary>
    public class MovingNoteSystemCached : GameSystemBase
    {
        private const float PerfectLineY = 4.0f;

        // Tunable block size for cache-friendly processing
        private const int PROCESS_BLOCK_SIZE = 64;

        protected override void Initialize()
        {
            Debug.Log("MovingNoteSystemCached initialized with cache-aware processing");
        }

        protected override void Execute(float deltaTime)
        {
            // Using the cache-optimized block-based processing
            // This approach is more cache-friendly as it processes entities in blocks
            // that are sized to fit efficiently in CPU cache
            World
                .CreateQuery()
                .ForEachBlock<TransformComponent, MusicNoteComponent, ActiveStateComponent>(
                    (
                        ref TransformComponent transform,
                        ref MusicNoteComponent note,
                        ref ActiveStateComponent active,
                        int entityId
                    ) =>
                    {
                        if (!active.IsActive)
                            return;

                        // Store previous position for interpolation
                        transform.PreviousPosition = transform.Position;

                        // Move the note
                        //transform.Position.y += note.Speed * deltaTime;

                        // Check if note has passed the screen
                        if (transform.Position.y > 6.0f)
                        {
                            // Deactivate the note
                            active.IsActive = false;
                        }

                        // Check if note is near the perfect line
                        if (Math.Abs(transform.Position.y - PerfectLineY) < 0.1f)
                        {
                            // Here you could trigger highlighting or effects
                        }
                    },
                    PROCESS_BLOCK_SIZE
                ); // Specify the block size for optimal cache utilization

            // Alternative approach using direct block access
            // This is even more efficient but requires more careful handling
            World
                .CreateQuery()
                .ForEachBlockDirect<TransformComponent, MusicNoteComponent, ActiveStateComponent>(
                    (
                        TransformComponent[] transforms,
                        MusicNoteComponent[] notes,
                        ActiveStateComponent[] actives,
                        int[] entityIds,
                        int startIndex,
                        int count
                    ) =>
                    {
                        // Process the entire block in one go
                        for (int i = 0; i < count; i++)
                        {
                            int index = startIndex + i;

                            // Skip inactive entities
                            if (!actives[index].IsActive)
                                continue;

                            // Store previous position
                            transforms[index].PreviousPosition = transforms[index].Position;

                            // Move the note
                            // transforms[index].Position.y += notes[index].Speed * deltaTime;

                            // Check if note has passed the screen
                            if (transforms[index].Position.y > 6.0f)
                            {
                                actives[index].IsActive = false;
                            }
                        }
                    },
                    PROCESS_BLOCK_SIZE
                );
        }

        // For comparison, here's the traditional approach
        private void TraditionalApproach(float deltaTime)
        {
            World
                .CreateQuery()
                .ForEach<TransformComponent, MusicNoteComponent, ActiveStateComponent>(
                    (
                        ref TransformComponent transform,
                        ref MusicNoteComponent note,
                        ref ActiveStateComponent active,
                        int entityId
                    ) =>
                    {
                        if (!active.IsActive)
                            return;

                        // Store previous position for interpolation
                        transform.PreviousPosition = transform.Position;

                        // Move the note
                        //transform.Position.y += note.Speed * deltaTime;

                        // Check if note has passed the screen
                        if (transform.Position.y > 6.0f)
                        {
                            // Deactivate the note
                            active.IsActive = false;
                        }

                        // Check if note is near the perfect line
                        if (Math.Abs(transform.Position.y - PerfectLineY) < 0.1f)
                        {
                            // Here you could trigger highlighting or effects
                        }
                    }
                );
        }

        // Performance test method that compares both approaches
        public void RunPerformanceTest(float deltaTime, int iterations = 10)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            // Warm up
            for (int i = 0; i < 3; i++)
            {
                TraditionalApproach(deltaTime);
                Execute(deltaTime);
            }

            // Test traditional approach
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                TraditionalApproach(deltaTime);
            }
            stopwatch.Stop();
            long traditionalTime = stopwatch.ElapsedTicks;

            stopwatch.Reset();

            // Test cache-aware approach
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                Execute(deltaTime);
            }
            stopwatch.Stop();
            long cacheAwareTime = stopwatch.ElapsedTicks;

            // Log results
            Debug.Log($"Performance Test Results (average over {iterations} iterations):");
            Debug.Log($"Traditional: {traditionalTime / iterations} ticks per iteration");
            Debug.Log($"Cache-Aware: {cacheAwareTime / iterations} ticks per iteration");
            Debug.Log($"Improvement: {(float)traditionalTime / cacheAwareTime:F2}x faster");
        }
    }
}

using System.Linq;
using UnityEngine;

/// <summary>
/// A system-like controller that manages multiple UI elements with burst movement behavior.
/// This controller follows ECS principles by separating data (BurstMovementData) from behavior
/// while maintaining MonoBehaviour compatibility.
/// </summary>
/// <remarks>
/// The burst movement creates a dynamic motion effect where elements:
/// 1. Start with an initial speed
/// 2. Accelerate smoothly to maximum speed
/// 3. Maintain that speed for a configurable distance
/// 4. Decelerate naturally to a stop
///
/// The controller processes all movements in batches for better performance
/// and provides default values that can be overridden per element.
/// </remarks>
public class BurstMovementUIController : MonoBehaviour
{
    private BurstMovementElement[] movementElements;

    [Header("Default Values")]
    [SerializeField]
    private float defaultMaxSpeed = 500f;

    [SerializeField]
    private float defaultAccelerationFactor = 2f;

    [SerializeField]
    private float defaultDecelerationFactor = 1.5f;

    [SerializeField]
    private float defaultBurstEndPercentage = 50f;

    [SerializeField]
    private float defaultInitialSpeedPercent = 0.1f;

    private Vector2[] cachedStartPosition;

    /// <summary>
    /// Initializes all movement elements when the controller starts.
    /// Sets up initial positions and applies default values where needed.
    /// </summary>
    public void InitializeElement(BurstMovementElement[] burstMovementElements)
    {
        movementElements = burstMovementElements;
        cachedStartPosition = new Vector2[movementElements.Length];
        int index = 0;
        foreach (var element in movementElements)
        {
            // Store initial position and normalize direction for consistent movement
            element.data.startPosition = element.target.anchoredPosition;
            element.data.direction = element.data.direction.normalized;
            element.data.hasStarted = true;
            element.data.isFinished = false;
            cachedStartPosition[index] = element.target.anchoredPosition;
            index++;

            ApplyDefaultsIfNeeded(ref element.data);
        }
        //Make all elements not move at start
        StopAll();
    }

    /// <summary>
    /// Applies default values to any unset or invalid movement parameters.
    /// This ensures all elements have valid movement settings even if not explicitly configured.
    /// </summary>
    /// <param name="data">Reference to the movement data to validate and update</param>
    private void ApplyDefaultsIfNeeded(ref BurstMovementData data)
    {
        if (data.maxSpeed <= 0)
            data.maxSpeed = defaultMaxSpeed;
        if (data.accelerationFactor <= 0)
            data.accelerationFactor = defaultAccelerationFactor;
        if (data.decelerationFactor <= 0)
            data.decelerationFactor = defaultDecelerationFactor;
        if (data.burstEndPercentage <= 0)
            data.burstEndPercentage = defaultBurstEndPercentage;
        if (data.initialSpeedPercent <= 0)
            data.initialSpeedPercent = defaultInitialSpeedPercent;
    }

    /// <summary>
    /// Processes movement updates for all active elements each frame.
    /// Follows a system-like approach by batch processing all movements.
    /// </summary>
    private void Update()
    {
        if (movementElements.Length == 0)
            return;

        foreach (var element in movementElements)
        {
            if (!element.data.hasStarted || element.data.isFinished)
                continue;
            ProcessMovement(element);
        }
    }

    /// <summary>
    /// Updates the position of a single movement element based on its current state and configuration.
    /// </summary>
    /// <param name="element">The movement element to process</param>
    private void ProcessMovement(BurstMovementElement element)
    {
        // Calculate how far we've moved as a percentage of total distance
        float distanceTraveled = Vector2.Distance(
            element.target.anchoredPosition,
            element.data.startPosition
        );
        float distancePercentage = (distanceTraveled / element.data.maxDistance) * 100f;

        // Check if we've reached our destination
        if (distancePercentage >= 100f)
        {
            element.data.isFinished = true;
            return;
        }

        // Calculate and apply movement for this frame
        element.data.currentSpeed = CalculateSpeed(distancePercentage, element.data);
        Vector2 newPosition =
            (Vector2)element.target.anchoredPosition
            + element.data.direction * (element.data.currentSpeed * Time.deltaTime);

        element.target.anchoredPosition = newPosition;
    }

    /// <summary>
    /// Calculates the current speed based on distance percentage using exponential curves
    /// for smooth acceleration and deceleration.
    /// </summary>
    /// <param name="distancePercentage">Current distance as percentage of total (0-100)</param>
    /// <param name="data">Movement configuration data</param>
    /// <returns>The calculated speed for the current frame</returns>
    /// <remarks>
    /// The speed calculation uses two different exponential curves:
    /// - Acceleration: speed = maxSpeed * (initial + (1-initial) * (1-e^(-aF * x)))
    /// - Deceleration: speed = maxSpeed * e^(-dF * x)
    /// Where:
    /// - aF = acceleration factor
    /// - dF = deceleration factor
    /// - x = progress (0 to 1) in current phase
    /// </remarks>
    private float CalculateSpeed(float distancePercentage, BurstMovementData data)
    {
        // Acceleration phase
        if (distancePercentage <= data.burstEndPercentage)
        {
            float accelerationProgress = distancePercentage / data.burstEndPercentage;
            // Use exponential approach for smooth acceleration
            return data.maxSpeed
                * (
                    data.initialSpeedPercent
                    + (1f - data.initialSpeedPercent)
                        * (1f - Mathf.Exp(-data.accelerationFactor * accelerationProgress))
                );
        }
        // Deceleration phase
        else
        {
            float decelerationProgress =
                (distancePercentage - data.burstEndPercentage) / (100f - data.burstEndPercentage);
            // Use exponential decay for natural-feeling deceleration
            return data.maxSpeed * Mathf.Exp(-data.decelerationFactor * decelerationProgress);
        }
    }

    /// <summary>
    /// Starts movement for all elements in the controller.
    /// </summary>
    public void StartAll()
    {
        foreach (var element in movementElements)
        {
            element.data.hasStarted = true;
            element.data.isFinished = false;
        }
    }

    /// <summary>
    /// Stops movement for all elements in the controller.
    /// </summary>
    public void StopAll()
    {
        foreach (var element in movementElements)
        {
            element.data.hasStarted = false;
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < cachedStartPosition.Length; i++)
        {
            movementElements[i].target.anchoredPosition = cachedStartPosition[i];
        }
    }

    /// <summary>
    /// Represents the configuration and runtime data for a single burst movement.
    /// Separates data from behavior following ECS principles.
    /// </summary>
    [System.Serializable]
    public struct BurstMovementData
    {
        public Vector2 direction; // Movement direction (will be normalized)
        public float maxDistance; // Total distance to travel

        public float maxSpeed; // Maximum movement speed during burst

        [Range(0.5f, 5)]
        public float accelerationFactor; // Controls how quickly max speed is reached

        [Range(0.5f, 5)]
        public float decelerationFactor; // Controls how quickly speed reduces after burst

        [Range(0f, 70f)]
        public float burstEndPercentage; // When to transition from burst to deceleration (% of total distance)

        [Range(0, 1)]
        public float initialSpeedPercent; // Starting speed as percentage of max speed

        // Runtime data - hidden from inspector
        [HideInInspector]
        public Vector2 startPosition;

        [HideInInspector]
        public float currentSpeed;

        [HideInInspector]
        public bool hasStarted;

        [HideInInspector]
        public bool isFinished;
    }

    /// <summary>
    /// Combines a UI element target with its movement configuration.
    /// </summary>
    [System.Serializable]
    public class BurstMovementElement
    {
        public RectTransform target; // The UI element to move
        public BurstMovementData data; // Movement configuration and runtime data
    }
}

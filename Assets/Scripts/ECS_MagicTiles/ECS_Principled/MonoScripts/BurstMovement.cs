using UnityEngine;

/// <summary>
/// Implements a customizable burst movement behavior where an object accelerates to a maximum speed,
/// maintains that speed for a configurable distance, then smoothly decelerates to a stop.
/// The movement uses exponential functions to create natural-feeling acceleration and deceleration.
/// </summary>
/// <remarks>
/// The movement is divided into two phases:
/// 1. Burst Phase: Object accelerates from initial speed to max speed using a smooth exponential curve
/// 2. Deceleration Phase: Object gradually slows down to a stop using natural exponential decay
///
/// Mathematical formulas used:
/// - Acceleration: speed = maxSpeed * (initialPercent + (1-initialPercent) * (1-e^(-aF * x)))
/// - Deceleration: speed = maxSpeed * e^(-dF * x)
/// Where:
/// - aF = acceleration factor
/// - dF = deceleration factor
/// - x = progress (0 to 1) in current phase
/// </remarks>
public class BurstMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField]
    private Vector3 direction = Vector3.forward;

    [SerializeField]
    private float maxDistance = 10f;

    [Header("Speed Properties")]
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField, Range(0.5f, 5f)]
    private float accelerationFactor = 2f;

    [SerializeField, Range(0.5f, 5f)]
    private float decelerationFactor = 1.5f;

    [Header("Burst Configuration")]
    [SerializeField, Range(0f, 100f)]
    private float burstEndPercentage = 50f;

    [SerializeField, Range(0f, 1f)]
    private float initialSpeedPercent = 0.1f;

    private Vector3 startPosition;
    private float currentSpeed;
    private bool hasStarted;

    /// <summary>
    /// Initializes the movement component by storing the starting position and normalizing the direction vector.
    /// </summary>
    private void Start()
    {
        startPosition = transform.position;
        direction = direction.normalized;
        hasStarted = true;

        Debug.Log(
            $"=== Initial Parameters ===\n"
                + $"Max Speed: {maxSpeed}\n"
                + $"Max Distance: {maxDistance}\n"
                + $"Acceleration Factor: {accelerationFactor}\n"
                + $"Deceleration Factor: {decelerationFactor}\n"
                + $"Direction: {direction}"
        );
    }

    /// <summary>
    /// Updates the object's position each frame based on the current movement phase and calculated speed.
    /// Handles progression through acceleration and deceleration phases until reaching the target distance.
    /// </summary>
    private void Update()
    {
        if (!hasStarted)
            return;

        float distanceTraveled = Vector3.Distance(transform.position, startPosition);
        float distancePercentage = (distanceTraveled / maxDistance) * 100f;

        if (distancePercentage >= 100f)
        {
            hasStarted = false;
            return;
        }

        currentSpeed = CalculateSpeed(distancePercentage);
        transform.position += direction * (currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the current speed based on the percentage of total distance traveled.
    /// Uses different mathematical formulas for acceleration and deceleration phases.
    /// </summary>
    /// <param name="distancePercentage">Current distance traveled as a percentage (0-100) of total distance</param>
    /// <returns>The calculated speed for the current frame</returns>
    /// <remarks>
    /// During acceleration (0% to burstEndPercentage):
    /// - Starts at initialSpeedPercent of maxSpeed
    /// - Smoothly accelerates to maxSpeed using exponential approach
    ///
    /// During deceleration (burstEndPercentage to 100%):
    /// - Starts at maxSpeed
    /// - Exponentially decays to zero
    /// </remarks>
    private float CalculateSpeed(float distancePercentage)
    {
        // Acceleration phase
        if (distancePercentage <= burstEndPercentage)
        {
            float accelerationProgress = distancePercentage / burstEndPercentage;
            return maxSpeed
                * (
                    initialSpeedPercent
                    + (1f - initialSpeedPercent)
                        * (1f - Mathf.Exp(-accelerationFactor * accelerationProgress))
                );
        }
        // Deceleration phase
        else
        {
            float decelerationProgress =
                (distancePercentage - burstEndPercentage) / (100f - burstEndPercentage);
            return maxSpeed * Mathf.Exp(-decelerationFactor * decelerationProgress);
        }
    }

    /// <summary>
    /// Draws debug visualization in the Scene view to help understand the movement path and progress.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        // Draw the full path
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPosition, startPosition + direction * maxDistance);

        // Draw burst end point
        Vector3 burstEndPoint =
            startPosition + direction * (maxDistance * burstEndPercentage / 100f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(burstEndPoint, 0.3f);

        // Draw current position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}

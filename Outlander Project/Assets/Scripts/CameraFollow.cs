using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> celestialBodies;
    [SerializeField] private float positionFollowSpeed;
    [SerializeField] private float rotationFollowSpeed;
    [SerializeField] private float minOrthographicSize;
    [SerializeField] private float maxOrthographicSize;
    [SerializeField] private float switchDistance;

    private Camera mainCamera;
    private Transform activeCelestialBody;
    private float transitionProgress = 1f; // Add a transition progress variable (1 means fully transitioned)

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        FindClosestCelestialBody();
    }

    void FixedUpdate()
    {
        // Follow the target's position
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPosition, positionFollowSpeed);

        // Increment the transition progress
        transitionProgress += Time.deltaTime;

        if (activeCelestialBody != null)
        {
            // Use the position directly without checking for the parent
            Vector3 worldPosition = activeCelestialBody.position;

            // Rotate the camera to be perpendicular to the celestial body's surface with the bottom of the camera closer to the surface
            Vector3 directionToBodyCenter = (worldPosition - target.position).normalized;
            float targetAngle = Mathf.Atan2(directionToBodyCenter.y, directionToBodyCenter.x) * Mathf.Rad2Deg + 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationFollowSpeed * transitionProgress);

            // Set the orthographic size based on the distance to the celestial body
            float distanceToBody = Vector3.Distance(target.position, worldPosition);
            float distanceToSurface = distanceToBody - activeCelestialBody.localScale.x * 0.5f;
            float orthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, distanceToSurface / switchDistance);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, orthographicSize, positionFollowSpeed * transitionProgress);
        }
        else
        {
            // Reset rotation and orthographic size
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationFollowSpeed * transitionProgress);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxOrthographicSize, positionFollowSpeed * transitionProgress);
        }
    }

    private void FindClosestCelestialBody()
    {
        // Reset active celestial body
        Transform previousActiveCelestialBody = activeCelestialBody;
        activeCelestialBody = null;
        float minDistance = float.MaxValue;

        // Check the distance to each celestial body
        foreach (Transform celestialBody in celestialBodies)
        {
            // Use the position directly without checking for the parent
            Vector3 worldPosition = celestialBody.position;

            float distance = Vector3.Distance(target.position, worldPosition);
            float radius = celestialBody.localScale.x * 0.5f;

            if (distance < radius + switchDistance && distance < minDistance)
            {
                activeCelestialBody = celestialBody;
                minDistance = distance;
            }
        }

        // If the active celestial body has changed, reset the transition progress
        if (previousActiveCelestialBody != activeCelestialBody)
        {
            transitionProgress = 0f;
        }
    }
}


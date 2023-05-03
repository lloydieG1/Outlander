using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> celestialBodies;
    [SerializeField] private float positionFollowSpeed = 0.1f;
    [SerializeField] private float rotationFollowSpeed = 0.1f;
    [SerializeField] private float minOrthographicSize = 5f;
    [SerializeField] private float maxOrthographicSize = 10f;
    [SerializeField] private float radiusPercentage = 1.5f;

    private Camera mainCamera;
    private Transform activeCelestialBody;

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

        if (activeCelestialBody != null)
        {
            // Use the position directly without checking for the parent
            Vector3 worldPosition = activeCelestialBody.position;

            // Rotate the camera to be perpendicular to the celestial body's surface with the bottom of the camera closer to the surface
            Vector3 directionToBodyCenter = (worldPosition - target.position).normalized;
            float targetAngle = Mathf.Atan2(directionToBodyCenter.y, directionToBodyCenter.x) * Mathf.Rad2Deg + 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationFollowSpeed);

            // Set the orthographic size based on the distance to the celestial body
            float distanceToBody = Vector3.Distance(target.position, worldPosition);
            float distanceToSurface = distanceToBody - activeCelestialBody.localScale.x * 0.5f;
            float orthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, distanceToSurface / (activeCelestialBody.localScale.x * radiusPercentage));
            mainCamera.orthographicSize = orthographicSize;
        }
        else
        {
            // Reset rotation and orthographic size
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationFollowSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxOrthographicSize, positionFollowSpeed);
        }
    }

    private void FindClosestCelestialBody()
    {
        // Reset active celestial body
        activeCelestialBody = null;
        float minDistance = float.MaxValue;

        // Check the distance to each celestial body
        foreach (Transform celestialBody in celestialBodies)
        {
            // Use the position directly without checking for the parent
            Vector3 worldPosition = celestialBody.position;

            float distance = Vector3.Distance(target.position, worldPosition);
            float radius = celestialBody.localScale.x * 0.5f * radiusPercentage;

            if (distance < radius && distance < minDistance)
            {
                activeCelestialBody = celestialBody;
                minDistance = distance;
            }
        }
    }
}

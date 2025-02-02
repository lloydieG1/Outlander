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
        if (target != null)
        {
            // Follow the target's position
            Vector3 targetPosition = target.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, positionFollowSpeed);

            // increment the transition progress
            transitionProgress += Time.deltaTime;

            if (activeCelestialBody != null)
            {
                // use the position directly without checking for the parent
                Vector3 worldPosition = activeCelestialBody.position;

                // rotate the camera to be tangential to the celestial body's surface 
                Vector3 directionToBodyCenter = (worldPosition - target.position).normalized;
                float targetAngle = Mathf.Atan2(directionToBodyCenter.y, directionToBodyCenter.x) * Mathf.Rad2Deg + 90f;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationFollowSpeed * transitionProgress);

                //set the orthographic size based on the distance to the celestial body
                float distanceToBody = Vector3.Distance(target.position, worldPosition);
                float distanceToSurface = distanceToBody - activeCelestialBody.localScale.x * 0.5f;
                float orthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, distanceToSurface / switchDistance);
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, orthographicSize, positionFollowSpeed * transitionProgress);
            }
        }
        else
        {
            // reset rotation and orthographic size
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationFollowSpeed * transitionProgress);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxOrthographicSize, positionFollowSpeed * transitionProgress);
        }
    }


    private void FindClosestCelestialBody()
    {
        if (target == null)
            return;
            
        // reset active celestial body
        Transform previousActiveCelestialBody = activeCelestialBody;
        activeCelestialBody = null;
        float minDistance = float.MaxValue;

        // check the distance to each celestial body
        foreach (Transform celestialBody in celestialBodies)
        {
            // use the position directly without checking for the parent
            Vector3 worldPosition = celestialBody.position;

            float distance = Vector3.Distance(target.position, worldPosition);
            float radius = celestialBody.localScale.x * 0.5f;

            if (distance < radius + switchDistance && distance < minDistance)
            {
                activeCelestialBody = celestialBody;
                minDistance = distance;
            }
        }

        // if the active celestial body has changed, reset the transition progress
        if (previousActiveCelestialBody != activeCelestialBody)
        {
            transitionProgress = 0f;
        }
    }
}


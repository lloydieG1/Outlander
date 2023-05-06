using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static void AlignObjectToPlanetSurface(Transform target, Transform planet)
    {
        if (planet == null) return;

        // Calculate direction to planet's center
        Vector3 directionToBodyCenter = (planet.position - target.position).normalized;

        // Calculate target angle to align the bottom of the object to the planet's surface
        float targetAngle = Mathf.Atan2(directionToBodyCenter.y, directionToBodyCenter.x) * Mathf.Rad2Deg + 90f;

        // Set the target rotation
        target.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    public static Transform FindClosestCelestialBody(Transform target, List<Transform> celestialBodies)
    {
        Transform closestCelestialBody = null;
        float minDistance = float.MaxValue;

        foreach (Transform celestialBody in celestialBodies)
        {
            float distance = Vector3.Distance(target.position, celestialBody.position);

            if (distance < minDistance)
            {
                closestCelestialBody = celestialBody;
                minDistance = distance;
            }
        }

        return closestCelestialBody;
    }
}

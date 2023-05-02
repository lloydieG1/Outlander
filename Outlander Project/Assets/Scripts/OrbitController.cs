using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public float orbitSpeed = 10f; // Set the orbit speed.
    public float orbitRadius = 3f; // Set the orbit radius.

    private Transform _centralPlanet;

    private void Start()
    {
        _centralPlanet = transform.parent; // Get the central planet (parent).
        transform.localPosition = new Vector2(orbitRadius, 0f); // Set the initial position.
    }

    private void Update()
    {
        float angle = orbitSpeed * Time.deltaTime; // Calculate the angle change.
        RotateAround(_centralPlanet.position, angle); // Rotate around the central planet.
    }

    private void RotateAround(Vector2 center, float angle)
    {
        Vector2 direction = (Vector2)transform.position - center;
        direction = Quaternion.Euler(0, 0, angle) * direction;
        transform.position = center + direction;
    }
}


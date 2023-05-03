using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public float orbitSpeed; // Set the orbit speed.
    public Transform centralBody; // Set the central body to orbit around.

    private Vector2 initialPosition;

    private void Start()
    {
        if (centralBody == null)
        {
            Debug.LogError("Central body not assigned in the OrbitController component.");
            return;
        }

        initialPosition = transform.position; // Set the initial position based on the position in the Scene editor.
    }

    private void FixedUpdate()
    {
        if (centralBody == null) return;

        float angle = orbitSpeed * Time.deltaTime; // Calculate the angle change.
        RotateAround(centralBody.position, angle); // Rotate around the central planet.
    }

    private void RotateAround(Vector2 center, float angle)
    {
        Vector2 direction = (Vector2)transform.position - center;
        direction = Quaternion.Euler(0, 0, angle) * direction;
        transform.position = center + direction;
    }
}

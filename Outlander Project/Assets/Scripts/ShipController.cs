using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed = 10f; // Speed of ship thrust
    [SerializeField] private float rotateSpeed = 100f; // Speed of ship rotation

    private Rigidbody2D rb;
    private Vector3 thrustDirection = Vector2.up;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get the player's input for rotation and thrust
        float rotationInput = Input.GetAxis("Horizontal");
        float thrustInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            thrustInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            thrustInput = -1f;
        }

        // Rotate the ship based on player input
        float rotationAmount = rotationInput * rotateSpeed * Time.fixedDeltaTime;
        rb.rotation -= rotationAmount;

        // Apply thrust to the ship in the direction it's facing
        thrustDirection = Quaternion.AngleAxis(rotationInput * rotateSpeed * Time.deltaTime, Vector3.back) * thrustDirection;
        rb.AddForce(thrustDirection * thrustInput * thrustSpeed);
    }
}
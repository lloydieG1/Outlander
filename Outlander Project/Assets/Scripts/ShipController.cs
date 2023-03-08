using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed; // Speed of ship thrust
    [SerializeField] private float boostSpeed; // Speed of ship boost
    [SerializeField] private float rotateSpeed; // Speed of ship rotation
    [SerializeField] private float fuel; // Speed of ship rotation


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
        float strafeInput = Input.GetAxis("Horizontal");
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

        // strafing
        if (Input.GetKey(KeyCode.Q))
        {
            strafeInput = 1f;
            rb.AddForce(-transform.right * strafeInput * thrustSpeed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            strafeInput = -1f;
            rb.AddForce(transform.right * strafeInput * thrustSpeed);
        }


        // boost
        if (Input.GetKey(KeyCode.Space))
        {
            thrustInput = 1f;
            rb.AddForce(thrustDirection * thrustInput * boostSpeed);
        }

        // Rotate the ship based on player input
        float rotationAmount = rotationInput * rotateSpeed * Time.fixedDeltaTime;
        rb.rotation -= rotationAmount;

        // Apply thrust to the ship in the direction it's facing
        thrustDirection = Quaternion.AngleAxis(rotationInput * rotateSpeed * Time.deltaTime, Vector3.back) * thrustDirection;
        rb.AddForce(thrustDirection * thrustInput * thrustSpeed);
        
    }
}
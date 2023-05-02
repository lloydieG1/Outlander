using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class AttractionBehaviour : MonoBehaviour
{
    Rigidbody2D rigidBody; // Rigidbody2D component reference for the GameObject

    [SerializeField] bool isAttractor; // bool to check if this GameObject is an attractor
    [SerializeField] bool isAttractee; // bool to check if this GameObject is an attractee

    // initial velocity of the GameObject
    [SerializeField] Vector3 initialVelocity; 
    // bool to check if initial velocity should be applied on start
    [SerializeField] bool applyInitialVelocityOnStart; 

    // Getter and setter for isAttractee property
    public bool IsAttractee
    {
        get
        {
            return isAttractee;
        }
        set
        {
            if (value == true)
            {
                // Add the Rigidbody2D component to the attractees list if it's not already present
                if (!GravityManager.attractees.Contains(this.GetComponent<Rigidbody2D>()))
                {
                    GravityManager.attractees.Add(rigidBody);
                }

            }
            else if (value == false)
            {
                // Remove the Rigidbody2D component from the attractees list
                GravityManager.attractees.Remove(rigidBody);
            }
            isAttractee = value;
        }
    }

    // Getter and setter for isAttractor property
    public bool IsAttractor
    {
        get
        {
            return isAttractor;
        }
        set
        {
            if (value == true)
            {
                // Add the Rigidbody2D component to the attractors list if it's not already present
                if (!GravityManager.attractors.Contains(this.GetComponent<Rigidbody2D>()))
                {
                    GravityManager.attractors.Add(rigidBody);
                }
            }
            else if (value == false)
            {
                // Remove the Rigidbody2D component from the attractors list
                GravityManager.attractors.Remove(rigidBody);
            }
            isAttractor = value;
        }
    }

    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the GameObject
    }

    // Called when the script instance is being loaded
    void OnEnable()
    {
        IsAttractor = isAttractor;
        IsAttractee = isAttractee;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (applyInitialVelocityOnStart)
        {
            ApplyVelocity(initialVelocity); // Apply the initial velocity to the GameObject
        }

    }

    // Called when the behaviour becomes disabled or inactive
    void OnDisable()
    {
        // Remove the Rigidbody2D component from the attractors and attractees lists
        GravityManager.attractors.Remove(rigidBody);
        GravityManager.attractees.Remove(rigidBody);
    }

    // Apply the given velocity to the GameObject
    void ApplyVelocity(Vector3 velocity)
    {
        rigidBody.AddForce(initialVelocity, ForceMode2D.Impulse); // Add force to the Rigidbody2D component
    }
}
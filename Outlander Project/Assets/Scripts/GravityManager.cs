using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    //g is the gravity constant
    [SerializeField] float g = 1f;
    static float G;

    // in reality every body would be both attractor and attractee but i want to 
    // have control over which gameobjects attract or are attracted
    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();
    
    void Awake()
    {
        G=g; //in case g is changed in editor
    }

    void FixedUpdate()
    {
        // calculate gravity each frame
        SimulateGravities();
    }

    public static void SimulateGravities()
    {
        foreach(Rigidbody2D attractor in attractors)
        {
            foreach(Rigidbody2D attractee in attractees)
            {
                // prevent doing needless maths on a body attracting to itself
                if(attractor!=attractee)
                AddGravityForce(attractor,attractee);
            }
        }
    }

    // This function adds a gravitational force to a target object based on the position of an attractor object
    public static void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target)
    {
        // Get the Rigidbody2D component of the attractor
        Rigidbody2D attractorRb = attractor.GetComponent<Rigidbody2D>();

        // Get the position of the attractor and the target objects
        Vector2 attractorPosition = attractor.transform.position;
        Vector2 attracteePosition = target.transform.position;

        // Calculate the direction of the gravitational force
        Vector2 gravityDirection = (attractorPosition - attracteePosition).normalized;

        // Calculate the distance between the attractor and target objects
        float distance = Vector2.Distance(attractorPosition, attracteePosition);

        // Calculate the magnitude of the gravitational force
        float gravityMagnitude = G * target.mass * attractorRb.mass / Mathf.Pow(distance, 2f);

        // Apply the gravitational force to the target object
        target.AddForce(gravityMagnitude * gravityDirection);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] float g = 1f;
    static float G;

    //in physical universe every body would be both attractor and attractee
    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();
    public static bool isSimulatingLive = true;
    
    void Awake()
    {
        G=g;//in case g is changed in editor
    }

    void FixedUpdate()
    {
        
        if(isSimulatingLive)//PathHandler changes this
        SimulateGravities();
    }
    public static void SimulateGravities()
    {
        foreach(Rigidbody2D attractor in attractors)
        {
            foreach(Rigidbody2D attractee in attractees)
            {
                if(attractor!=attractee)
                AddGravityForce(attractor,attractee);
            }
        }
    }

    public static void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target)
    {
        Rigidbody2D attractorRb = attractor.GetComponent<Rigidbody2D>();
        Vector2 attractorPosition = attractor.transform.position;
        Vector2 attracteePosition = target.transform.position;
        Vector2 gravityDirection = (attractorPosition - attracteePosition).normalized;
        float distance = Vector2.Distance(attractorPosition, attracteePosition);
        float gravityMagnitude = G * target.mass * attractorRb.mass / Mathf.Pow(distance, 2f);
        target.AddForce(gravityMagnitude * gravityDirection);
    }
}

using UnityEngine;

public class Rigidbody2DMemento
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }

    public Rigidbody2DMemento(Vector2 position, Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}

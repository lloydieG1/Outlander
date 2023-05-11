using UnityEngine;

public class ThrustCommand : InputCommand
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;

    public ThrustCommand(Rigidbody2D rb, Vector2 direction, float speed)
    {
        this.rb = rb;
        this.direction = direction;
        this.speed = speed;
    }

    public void Execute()
    {
        rb.AddForce(direction * speed);
    }

    public void Undo()
    {
        rb.AddForce(-direction * speed);
    }
}

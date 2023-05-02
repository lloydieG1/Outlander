using UnityEngine;

public class StrafeCommand : InputCommand
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;

    public StrafeCommand(Rigidbody2D rb, Vector2 direction, float speed)
    {
        this.rb = rb;
        this.direction = direction;
        this.speed = speed;
    }

    public void Execute()
    {
        rb.AddForce(direction * speed);
    }
}
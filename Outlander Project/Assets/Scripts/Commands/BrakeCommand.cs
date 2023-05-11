using UnityEngine;

public class BrakeCommand : InputCommand
{
    private Rigidbody2D rb;
    private Vector2 brakingForce;

    public BrakeCommand(Rigidbody2D rb, Vector2 brakingForce)
    {
        this.rb = rb;
        this.brakingForce = brakingForce;
    }

    public void Execute()
    {
        rb.AddForce(brakingForce);
    }

    public void Undo()
    {
        rb.AddForce(-brakingForce);
    }
}

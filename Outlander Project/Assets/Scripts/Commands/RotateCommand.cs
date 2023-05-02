using UnityEngine;

public class RotateCommand : InputCommand
{
    private Rigidbody2D rb;
    private float rotationAmount;

    public RotateCommand(Rigidbody2D rb, float rotationAmount)
    {
        this.rb = rb;
        this.rotationAmount = rotationAmount;
    }

    public void Execute()
    {
        rb.rotation -= rotationAmount;
    }
}

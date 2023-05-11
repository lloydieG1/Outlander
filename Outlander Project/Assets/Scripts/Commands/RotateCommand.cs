using UnityEngine;

public class RotateCommand : InputCommand
{
    private Transform transform;
    private Quaternion previousRotation;
    private Quaternion targetRotation;

    public RotateCommand(Transform transform, Quaternion targetRotation)
    {
        this.transform = transform;
        this.targetRotation = targetRotation;
        this.previousRotation = transform.rotation;
    }

    public void Execute()
    {
        transform.rotation = targetRotation;
    }

    public void Undo()
    {
        transform.rotation = previousRotation;
    }
}

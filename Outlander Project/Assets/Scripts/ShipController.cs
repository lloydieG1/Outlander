using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float fuelConsumptionRate;
    [SerializeField] private float brakingForce;
    [SerializeField] private float rewindInterval;
    [SerializeField] private float rewindAmmoConsumptionRate;

    private Rigidbody2D rb;
    private float verticalInput;
    private float horizontalInput;
    private bool boost;
    private bool brake;
    private bool reverseCommands;
    private Stack<Rigidbody2DMemento> mementos;
    private float nextMementoTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mementos = new Stack<Rigidbody2DMemento>();
        nextMementoTime = Time.time + rewindInterval;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        boost = Input.GetKey(KeyCode.Space);
        brake = Input.GetKey(KeyCode.LeftShift);
        reverseCommands = Input.GetKey(KeyCode.R);
    }

    void FixedUpdate()
    {
        if (reverseCommands)
        {
            Rewind();
            ResourceManager.Instance.ConsumeAmmo(rewindAmmoConsumptionRate * Time.fixedDeltaTime);
        }
        else
        {
            if (Time.time >= nextMementoTime)
            {
                SaveMemento();
                nextMementoTime = Time.time + rewindInterval;
            }
            
            // Rotate to face the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            InputCommand rotateCommand = new RotateCommand(transform, targetRotation);
            rotateCommand.Execute();

            float fuel = ResourceManager.Instance.GetFuel();
            if (fuel <= 0) return;

            // Move in the direction of WASD input, considering camera rotation
            float currentThrustSpeed = boost ? boostSpeed : thrustSpeed;
            Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
            Vector3 worldMoveDirection = Camera.main.transform.TransformDirection(moveDirection);
            worldMoveDirection.z = 0; // Reset z value to 0 to maintain 2D movement
            worldMoveDirection.Normalize();

            if (moveDirection != Vector2.zero)
            {
                InputCommand thrustCommand = new ThrustCommand(rb, worldMoveDirection, currentThrustSpeed);
                if (reverseCommands)
                {
                    thrustCommand.Undo();
                }
                else
                {
                    thrustCommand.Execute();
                }
                ResourceManager.Instance.ConsumeFuel(fuelConsumptionRate * Time.fixedDeltaTime);
            }

            if (brake)
            {
                // Apply a braking force proportional to the current velocity
                Vector2 brakingForceVector = -rb.velocity * brakingForce;
                InputCommand brakeCommand = new BrakeCommand(rb, brakingForceVector);
                brakeCommand.Execute();

                // Consume fuel proportional to the braking force applied
                ResourceManager.Instance.ConsumeFuel(fuelConsumptionRate * Time.fixedDeltaTime);
            }
        }
    }

    void SaveMemento()
    {
        Rigidbody2DMemento memento = new Rigidbody2DMemento(rb.position, rb.velocity);
        mementos.Push(memento);
    }

    
    void Rewind()
    {
        if (mementos.Count > 0)
        {
            Rigidbody2DMemento memento = mementos.Pop();
            rb.position = memento.Position;
            rb.velocity = memento.Velocity;
        }
    }
}

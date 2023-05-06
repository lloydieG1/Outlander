using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float fuelConsumptionRate = 1.0f;

    private Rigidbody2D rb;
    private float rotationInput;
    private float thrustInput;
    private float strafeInput;
    private bool boost;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
        boost = Input.GetKey(KeyCode.Space);

        // Strafe input
        if (Input.GetKey(KeyCode.Q))
        {
            strafeInput = 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            strafeInput = -1f;
        }
        else
        {
            strafeInput = 0f;
        }
    }

    void FixedUpdate()
    {
        InputCommand rotateCommand = new RotateCommand(rb, rotationInput * rotateSpeed * Time.fixedDeltaTime);
        rotateCommand.Execute();

        float fuel = ResourceManager.Instance.GetFuel();
        if (fuel <= 0) return;

        if (thrustInput != 0)
        {
            float currentThrustSpeed = boost ? boostSpeed : thrustSpeed;
            InputCommand thrustCommand = new ThrustCommand(rb, transform.up, thrustInput * currentThrustSpeed);
            thrustCommand.Execute();

            ResourceManager.Instance.ConsumeFuel(fuelConsumptionRate * Mathf.Abs(thrustInput) * Time.fixedDeltaTime);
        }

        if (strafeInput != 0)
        {
            Vector2 strafeDirection = strafeInput > 0 ? -transform.right : transform.right;
            InputCommand strafeCommand = new StrafeCommand(rb, strafeDirection, Mathf.Abs(strafeInput) * strafeSpeed);
            strafeCommand.Execute();

            ResourceManager.Instance.ConsumeFuel(fuelConsumptionRate * Mathf.Abs(strafeInput) * Time.fixedDeltaTime);
        }
    }
}

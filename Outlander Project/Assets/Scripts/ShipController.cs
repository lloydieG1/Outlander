using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float fuelConsumptionRate = 1.0f;

    private Rigidbody2D rb;
    private float verticalInput;
    private float horizontalInput;
    private bool boost;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        boost = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        // Rotate to face the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);

        float fuel = ResourceManager.Instance.GetFuel();
        if (fuel <= 0) return;

        // Move in the direction of WASD input, considering camera rotation
        float currentThrustSpeed = boost ? boostSpeed : thrustSpeed;
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        Vector3 worldMoveDirection = Camera.main.transform.TransformDirection(moveDirection);
        worldMoveDirection.z = 0; // Reset z value to 0 to maintain 2D movement
        worldMoveDirection.Normalize();
        Vector2 force = worldMoveDirection * currentThrustSpeed;

        if (moveDirection != Vector2.zero)
        {
            rb.AddForce(force);
            ResourceManager.Instance.ConsumeFuel(fuelConsumptionRate * moveDirection.magnitude * Time.fixedDeltaTime);
        }
    }
}

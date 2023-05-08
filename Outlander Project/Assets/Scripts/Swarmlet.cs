using UnityEngine;

public class Swarmlet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int damage = 10;

    private GameObject target;
    private Health targetHealth;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = ((Vector2)target.transform.position - rb.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Swarmlet hit something");
        if (collision.gameObject.CompareTag("DrillCore"))
        {
            Debug.Log("Swarmlet hit DrillCore");
            if (targetHealth != null)
            {
                Debug.Log("Swarmlet hit DrillCore");
                targetHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        targetHealth = target.GetComponent<Health>();
    }
}

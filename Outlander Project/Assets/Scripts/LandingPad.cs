using System.Collections;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private float landingThreshold = 0.1f;
    [SerializeField] private float colliderReactivationDelay = 1f;
    private Rigidbody2D playerRigidbody;
    private bool isPlayerLanded = false;
    private Collider2D landingPadCollider;
    private bool canLand = true;

    private void Start()
    {
        landingPadCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship") && canLand)
        {
            Debug.Log("canLand: " + canLand);
            playerRigidbody = collision.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;
            collision.transform.SetParent(transform.parent);
            isPlayerLanded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship"))
        {
            Debug.Log("Spaceship left the landing pad");
            StartCoroutine(ReactivateColliderAfterDelay());
            collision.transform.SetParent(null);
            isPlayerLanded = false;
            
        }
    }

    private IEnumerator ReactivateColliderAfterDelay()
    {
        canLand = false;
        yield return new WaitForSeconds(colliderReactivationDelay);
        canLand = true;
    }

    private void Update()
    {
        if (isPlayerLanded)
        {
            // Check if the player is trying to take off
            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput > landingThreshold)
            {
                playerRigidbody = null;
                isPlayerLanded = false;
            }
        }
    }
}

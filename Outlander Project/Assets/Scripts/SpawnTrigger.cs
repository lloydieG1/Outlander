using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = Color.green;

    void OnDrawGizmos()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
    }
}

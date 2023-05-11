using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnCollision : MonoBehaviour
{
    [SerializeField] private string playerTag = "Spaceship";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // destroy the player
            Destroy(other.gameObject);
            ServiceLocator.Instance.GetService<AudioManager>().Play("Loss");

            //lose after a delay
            Invoke("Lose", 2f);
        }
    }

    public void Lose() {
        Debug.Log("LoseOnCollision.Lose()");
        LevelLoader.Instance.LoadLevel(0);
    }
}

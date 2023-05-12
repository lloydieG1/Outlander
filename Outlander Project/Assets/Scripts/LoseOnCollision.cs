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

            // unlock icarus achievement
            Debug.Log("Icarus achievement unlocked");
            Debug.Log(DataManager.Instance.currentProfile.playerName); 
            DataManager.Instance.UnlockAchievement("Icarus");

            PlayerLose.Instance.SelfDestructAndLoadMainMenu();
        }
    }

}

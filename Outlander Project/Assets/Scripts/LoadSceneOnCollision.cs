using UnityEngine;

public class LoadSceneOnCollision : MonoBehaviour
{
    [SerializeField] private string playerTag = "Spaceship";
    [SerializeField] private int mainSceneIndex = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            // Load the main scene with fade transition
            LevelLoader.Instance.LoadLevel(mainSceneIndex);
        }
    }
}

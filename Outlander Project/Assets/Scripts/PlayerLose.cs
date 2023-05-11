using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLose : MonoBehaviour
{
    public static PlayerLose Instance { get; private set; }

    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private float waitTime = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelfDestructAndLoadMainMenu()
    {
        if (playerShip != null)
        {
            Destroy(playerShip);
        }
        ServiceLocator.Instance.GetService<AudioManager>().Play("Loss");
        PauseManager.Instance.Resume();
        Invoke("LoadMainMenu", waitTime);
    }

    private void LoadMainMenu()
    {
        ResourceManager.Instance.Reset();
        WaveManager.Instance.Reset();
        LevelLoader.Instance.LoadLevel(sceneIndex);
    }
}

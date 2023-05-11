using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = 1;

    public void LoadScene()
    {
        PauseManager.Instance.Resume();
        Debug.Log("Loading scene " + sceneToLoad);
        LevelLoader.Instance.LoadLevel(sceneToLoad);
    }
}

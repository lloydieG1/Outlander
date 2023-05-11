using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int startingWave = 1;
    public static WaveManager Instance { get; private set; }

    public int CurrentWave { get; private set; }

    public delegate void WaveChange();
    public event WaveChange OnWaveChange;

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
            return;
        }

        GameEvents.Instance.OnIncreaseWave += IncreaseWave;
    }

    private void Start() {
        CurrentWave = startingWave;
        OnWaveChange?.Invoke();
    }

    public void IncreaseWave()
    {
        CurrentWave++;
        OnWaveChange?.Invoke();
    }

    //reset
    public void Reset()
    {
        CurrentWave = startingWave;
        OnWaveChange?.Invoke();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnIncreaseWave -= IncreaseWave;
    }
}

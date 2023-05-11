using UnityEngine;
using UnityEngine.UI;

public class WaveIndicator : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    private Text waveText;

    private void Awake()
    {
        waveText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateWaveText();
        waveManager.OnWaveChange += UpdateWaveText;
    }

    private void UpdateWaveText()
    {
        if(waveText != null && waveManager != null)
        {
            waveText.text = waveManager.CurrentWave.ToString();
        }
    }

    private void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.OnWaveChange -= UpdateWaveText;
        }
    }
}

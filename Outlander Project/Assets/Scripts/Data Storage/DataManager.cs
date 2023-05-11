using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public List<Achievement> achievements;
    public PlayerProfile playerProfile;

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
    }

    public void SaveGameData()
    {
        string achievementsJson = JsonUtility.ToJson(new SerializationWrapper<Achievement>(achievements), prettyPrint: true);
        PlayerPrefs.SetString("achievements", achievementsJson);

        string playerProfileJson = JsonUtility.ToJson(playerProfile, prettyPrint: true);
        PlayerPrefs.SetString("playerProfile", playerProfileJson);

        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("achievements"))
        {
            string achievementsJson = PlayerPrefs.GetString("achievements");
            achievements = JsonUtility.FromJson<SerializationWrapper<Achievement>>(achievementsJson).data;
        }
        else
        {
            // Initialize achievements list if it's the first time loading
            InitAchievements();
        }

        if (PlayerPrefs.HasKey("playerProfile"))
        {
            string playerProfileJson = PlayerPrefs.GetString("playerProfile");
            playerProfile = JsonUtility.FromJson<PlayerProfile>(playerProfileJson);
        }
        else
        {
            // Initialize player profile if it's the first time loading
            InitPlayerProfile();
        }
    }

    // public List<PlayerProfile> GetAvailableProfiles()
    // {
    // // Load all available profiles from PlayerPrefs, deserialize them, and return the list.
    // }

    public void InitAchievements()
    {
        achievements = new List<Achievement>
        {
            new Achievement
            {
                name = "First Steps",
                description = "Complete the tutorial level.",
                isUnlocked = false
            },
            new Achievement
            {
                name = "Experienced",
                description = "Reach level 10.",
                isUnlocked = false
            },
            // Add more achievements here
        };
    }

    public void InitPlayerProfile()
    {
        playerProfile = new PlayerProfile
        {
            playerName = "Player",
            playerLevel = 1,
            playerExperience = 0,
            // Set default values for other player-related data
        };
    }

}

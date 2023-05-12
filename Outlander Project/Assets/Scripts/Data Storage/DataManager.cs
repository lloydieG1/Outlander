using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public List<Achievement> achievements;
    public PlayerProfile currentProfile;
    public List<PlayerProfile> playerProfiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("isFirstTime"))
            {
                PlayerPrefs.SetInt("isFirstTime", 1);
                InitAchievements();
            }

            LoadGameData();
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

        string playerProfilesJson = JsonUtility.ToJson(new SerializationWrapper<PlayerProfile>(playerProfiles), prettyPrint: true);
        PlayerPrefs.SetString("playerProfiles", playerProfilesJson);

        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("achievements"))
        {
            string achievementsJson = PlayerPrefs.GetString("achievements");
            achievements = JsonUtility.FromJson<SerializationWrapper<Achievement>>(achievementsJson).data;
        }

        if (PlayerPrefs.HasKey("playerProfiles"))
        {
            string playerProfilesJson = PlayerPrefs.GetString("playerProfiles");
            playerProfiles = JsonUtility.FromJson<SerializationWrapper<PlayerProfile>>(playerProfilesJson).data;
        }
        else
        {
            // Initialize player profiles list if it's the first time loading
            playerProfiles = new List<PlayerProfile>();
        }
    }

    public void InitAchievements()
    {
        achievements = new List<Achievement>
        {
            new Achievement
            {
                name = "Icarus",
                description = "Die from flying into the sun",
                isUnlocked = false
            },
            new Achievement
            {
                name = "Existential Horror",
                description = "Run out of fuel",
                isUnlocked = false
            },
            // Add more achievements here
        };

        SaveGameData();
    }

    public void UnlockAchievement(string name)
    {
        Achievement achievement = DataManager.Instance.currentProfile.achievements.Find(a => a.name == name);
        if (achievement != null)
        {
            achievement.isUnlocked = true;
            SaveGameData();
        }
        else
        {
            Debug.LogError("Achievement " + name + " not found.");
        }
    }


}

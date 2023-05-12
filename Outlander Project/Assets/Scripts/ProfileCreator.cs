using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProfileCreator : MonoBehaviour
{
    public InputField playerNameInputField;
    public ProfileSelector profileSelector;

    public void CreateProfile()
    {
        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            List<Achievement> achievementsCopy = new List<Achievement>();
            foreach (Achievement achievement in DataManager.Instance.achievements)
            {
                achievementsCopy.Add(new Achievement
                {
                    name = achievement.name,
                    description = achievement.description,
                    isUnlocked = achievement.isUnlocked
                });
            }

            PlayerProfile newProfile = new PlayerProfile
            {
                playerName = playerName,
                highScore = 0,
                achievements = achievementsCopy
            };
            DataManager.Instance.playerProfiles.Add(newProfile);
            DataManager.Instance.SaveGameData();
        }
        else
        {
            Debug.LogError("Player name cannot be empty");
        }

        profileSelector.PopulateProfileList();
    }
}

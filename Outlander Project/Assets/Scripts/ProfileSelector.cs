using UnityEngine;
using UnityEngine.UI;

public class ProfileSelector : MonoBehaviour
{
    public GameObject profileButtonPrefab;
    public Transform profileButtonParent;
    public AchievementDisplay achievementDisplay;
    //public Text currentProfileText;

    private Button currentProfileButton;

    private void Start()
    {
        PopulateProfileList();
    }

public GameObject deleteButtonPrefab; 

public void PopulateProfileList()
{
    // Clear old buttons
    foreach (Transform child in profileButtonParent)
    {
        Destroy(child.gameObject);
    }

    // Create new buttons
    foreach (PlayerProfile profile in DataManager.Instance.playerProfiles)
    {
        GameObject buttonObject = Instantiate(profileButtonPrefab, profileButtonParent);
        Button button = buttonObject.GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = profile.playerName + " - High score: " + profile.highScore;

        button.onClick.AddListener(() =>
        {
            SelectProfile(profile, button);
        });

        // Add the delete button and its functionality
        GameObject deleteButtonObject = Instantiate(deleteButtonPrefab, buttonObject.transform);
        Button deleteButton = deleteButtonObject.GetComponent<Button>();
        deleteButton.onClick.AddListener(() =>
        {
            DeleteProfile(profile);
        });
    }
}

public void DeleteProfile(PlayerProfile profile)
{
    DataManager.Instance.playerProfiles.Remove(profile);
    DataManager.Instance.SaveGameData();

    // If the deleted profile was the current profile, set the current profile to null
    if (DataManager.Instance.currentProfile == profile)
    {
        DataManager.Instance.currentProfile = null;
    }

    PopulateProfileList(); // Refresh the profile list
}


    public void SelectProfile(PlayerProfile profile, Button button)
    {
        // Highlight the selected button and unhighlight the previously selected button
        if (currentProfileButton != null)
        {
            ColorBlock colors = currentProfileButton.colors;
            colors.normalColor = Color.white;
            currentProfileButton.colors = colors;
        }

        ColorBlock newColors = button.colors;
        newColors.normalColor = Color.green;
        button.colors = newColors;

        currentProfileButton = button;

        // Set the selected profile as the active profile
        DataManager.Instance.currentProfile = profile;

        // Display the selected profile's name
        //currentProfileText.text = "Current profile: " + profile.playerName;

        // Populate the achievement list
        achievementDisplay.PopulateAchievementList();

        // You may want to navigate to another screen here, e.g. the main menu
    }
}

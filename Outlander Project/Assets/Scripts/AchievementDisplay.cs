using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public GameObject achievementItemPrefab;
    public Transform achievementListParent;

    private void Start()
    {
        if (DataManager.Instance.currentProfile != null)
        {
            PopulateAchievementList();
        }
    }

    public void PopulateAchievementList()
    {
        // Clear old items
        foreach (Transform child in achievementListParent)
        {
            Destroy(child.gameObject);
        }

        // Create new items
        foreach (Achievement achievement in DataManager.Instance.currentProfile.achievements)
        {
            GameObject itemObject = Instantiate(achievementItemPrefab, achievementListParent);
            AchievementItem item = itemObject.GetComponent<AchievementItem>();

            item.Setup(achievement);
        }
    }

}

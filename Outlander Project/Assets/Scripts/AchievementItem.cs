using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Image completionStatus;

    public void Setup(Achievement achievement)
    {
        nameText.text = achievement.name;
        descriptionText.text = achievement.description;
        completionStatus.enabled = achievement.isUnlocked;
    }
}

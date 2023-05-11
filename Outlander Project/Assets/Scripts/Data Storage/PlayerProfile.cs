using System.Collections.Generic;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    public int playerLevel;
    public int playerExperience;

    public List<Achievement> achievements; 
}
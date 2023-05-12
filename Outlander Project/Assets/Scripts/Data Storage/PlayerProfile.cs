using System.Collections.Generic;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    public int highScore;

    public List<Achievement> achievements; 
}
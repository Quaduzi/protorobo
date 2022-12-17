using UnityEngine;

public class SaveManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("CompletedLevels"))
        {
            var completedLevels = PlayerPrefs.GetInt("CompletedLevels");
            Debug.Log($"Completed Levels: {completedLevels}");
        }
        else
        {
            ResetCompletedLevelsCount();
            Debug.Log("PlayerPrefs created");
        }
    }

    public static int LoadCompletedLevelsCount()
    {
        return PlayerPrefs.HasKey("CompletedLevels") ? PlayerPrefs.GetInt("CompletedLevels") : 0;
    }

    public static void SaveCompletedLevelsCount(int levelsCount)
    {
        var saved = LoadCompletedLevelsCount();
        if (saved < levelsCount) PlayerPrefs.SetInt("CompletedLevels", levelsCount);
    }
    
    public static void ResetCompletedLevelsCount()
    {
        PlayerPrefs.SetInt("CompletedLevels", 0);
    }
}

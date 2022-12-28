using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string levelTitle;
    
    public struct LevelInfo
    {
        public string levelTitle;
        public int levelIndex;
    }
    void Start()
    {
        GlobalEventManager.SendLevelLoaded(
            new LevelInfo 
            { 
                levelIndex = SceneControl.CurrentLevel, 
                levelTitle = levelTitle
            }
        );
    }
}

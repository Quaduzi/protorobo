using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{   
    [SerializeField] private int firstLevelBuildIndex;
    [SerializeField] private int mainMenuBuildIndex;
    [SerializeField] private int titlesBuildIndex;
    [SerializeField] private int selectLevelBuildIndex;

    private int _currentLevel;
    
    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Start()
    {
        LoadMainMenu();
    }
    
    private static SceneControl _instance;
    public static int LevelsCount => SceneManager.sceneCountInBuildSettings - _instance.firstLevelBuildIndex;
    
    public static void LoadMainMenu() => LoadScene(_instance.mainMenuBuildIndex);
    public static void LoadTitles() => LoadScene(_instance.titlesBuildIndex);
    public static void LoadSelectLevel() => LoadScene(_instance.selectLevelBuildIndex);

    [ContextMenu("bruh")]
    public void Test() => LoadLevel(1);

    public static void NextLevel()
    {
        if (++_instance._currentLevel < LevelsCount) LoadLevel(_instance._currentLevel);
        else LoadTitles();
    }
    public static void LoadLevel(int levelIndex)
    {
        if (levelIndex > LevelsCount || levelIndex < 0) return;
        LoadScene(_instance.firstLevelBuildIndex + levelIndex);
        GlobalEventManager.SendLevelChanged(levelIndex);
        _instance._currentLevel = levelIndex;
    }
    
    private static void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    
}
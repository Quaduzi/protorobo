using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private int mainMenuBuildIndex;
    [SerializeField] private int titlesBuildIndex;

    private List<Scene> _levels;
    private Scene _currentScene;

    public int LevelsCount => _levels.Count;

    private void Awake()
    {
        _levels = GetScenesFromBuild("^Level");
        LoadMainMenu();
    }

    public void LoadMainMenu() => LoadScene(mainMenuBuildIndex);
    public void LoadTitles() => LoadScene(titlesBuildIndex);

    [ContextMenu("bruh")]
    public void Test() => SwitchScene(3);
    public void LoadLevel(int index)
    {
        if (index > LevelsCount || index < 0) return;
        SwitchScene(_levels[index].buildIndex);
    }
    
    private void SwitchScene(int buildIndex)
    {
        SceneManager.UnloadSceneAsync(_currentScene);
        LoadScene(buildIndex);
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        _currentScene = SceneManager.GetSceneByBuildIndex(buildIndex);
    }

    private List<Scene> GetScenesFromBuild(string namePattern)
    {
        var scenes = new List<Scene>();
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scene = SceneManager.GetSceneByBuildIndex(i);
            if (Regex.IsMatch(scene.ToString(), namePattern)) scenes.Add(scene);
        }
        return scenes;
    }
}

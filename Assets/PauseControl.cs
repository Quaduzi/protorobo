using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [ContextMenu("Pause")]
    private void Start()
    {
        GlobalEventManager.OnCardBeginDrag.AddListener(PauseGame);
        GlobalEventManager.OnCardEndDrag.AddListener(UnpauseGame);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    [ContextMenu("Unause")]
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}

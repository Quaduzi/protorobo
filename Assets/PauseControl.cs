using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [ContextMenu("Pause")]
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

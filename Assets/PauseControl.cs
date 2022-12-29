using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [ContextMenu("Pause")]
    private void Start()
    {
        GlobalEventManager.OnCardBeginDrag.AddListener(PauseGame);
        GlobalEventManager.OnStartLevelTransition.AddListener(PauseGame);
        GlobalEventManager.OnDeath.AddListener(PauseGame);
        GlobalEventManager.OnCardEndDrag.AddListener(UnpauseGame);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    [ContextMenu("Unpause")]
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}

using UnityEngine;

public class DisableButtons : MonoBehaviour
{
    private int _completedLevels;
    private UnityEngine.UI.Button[] _buttons;
    void Start()
    {
        _buttons = GetComponentsInChildren<UnityEngine.UI.Button>();
        _completedLevels = SaveManager.LoadCompletedLevelsCount();
        LockLevelsButtons();
    }

    public void LockLevelsButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i > _completedLevels) _buttons[i].interactable = false;
        }
    }
}

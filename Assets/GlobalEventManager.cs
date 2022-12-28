using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static readonly UnityEvent OnCardBeginDrag = new();
    public static readonly UnityEvent OnCardEndDrag = new();
    public static readonly UnityEvent<int> OnLevelChanged = new();

    public static void SendCardBeginDrag() => OnCardBeginDrag?.Invoke();
    public static void SendCardEndDrag() => OnCardEndDrag?.Invoke();
    public static void SendLevelChanged(int level) => OnLevelChanged?.Invoke(level);
}

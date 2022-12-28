using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static readonly UnityEvent OnCardBeginDrag = new();
    public static readonly UnityEvent OnCardEndDrag = new();
    public static readonly UnityEvent OnStartCardTransition = new();
    public static readonly UnityEvent OnEndCardTransition = new();
    public static readonly UnityEvent OnStartLevelTransition = new();
    public static readonly UnityEvent<int> OnLevelChanged = new();

    public static void SendCardBeginDrag() => OnCardBeginDrag?.Invoke();
    public static void SendCardEndDrag() => OnCardEndDrag?.Invoke();
    public static void SendStartCardTransition() => OnStartCardTransition?.Invoke();
    public static void SendEndCardTransition() => OnEndCardTransition?.Invoke();
    public static void SendStartLevelTransition() => OnStartLevelTransition?.Invoke();
    
    public static void SendLevelChanged(int level) => OnLevelChanged?.Invoke(level);
}

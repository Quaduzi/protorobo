using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static readonly UnityEvent OnCardBeginDrag = new();
    public static readonly UnityEvent OnCardEndDrag = new();

    public static void SendCardBeginDrag() => OnCardBeginDrag?.Invoke();
    public static void SendCardEndDrag() => OnCardEndDrag?.Invoke();
}

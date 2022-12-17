using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnCardBeginDrag = new UnityEvent();
    public static UnityEvent OnCardEndDrag = new UnityEvent();
    public static UnityEvent OnStartTransition = new UnityEvent();
    public static UnityEvent OnEndTransition = new UnityEvent();
    public static UnityEvent OnSlotChanged = new UnityEvent();
    public static UnityEvent OnGameStarted = new UnityEvent();

    public static void SendCardBeginDrag()
    {
        OnCardBeginDrag?.Invoke();
    }
    public static void SendCardEndDrag()
    {
        OnCardEndDrag?.Invoke();
    }
    public static void SendStartTransition()
    {
        OnStartTransition?.Invoke();
    }
    public static void SendEndTransition()
    {
        OnEndTransition?.Invoke();
    }
    public static void SendSlotChanged()
    {
        OnEndTransition?.Invoke();
    }
    public static void SendGameStarted()
    {
        OnGameStarted?.Invoke();
    }
}

using System;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out _)) GlobalEventManager.SendStartCardTransition();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _)) GlobalEventManager.SendEndCardTransition();
    }
}

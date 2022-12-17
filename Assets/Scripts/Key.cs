using UnityEngine;

public class Key : MonoBehaviour
{
    public Card unlockCard;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        unlockCard.lockType = Card.CardLockType.Unlock;
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(SpriteRenderer))]
public class Lock : MonoBehaviour
{
    [SerializeField] private Sprite hardLockSprite;
    [SerializeField] private Sprite keyLockSprite;
    [SerializeField] private LockType lockType;
    [SerializeField] private GameObject unlockEffect;

    private SpriteRenderer _spriteRenderer;
    private Card _card;
    public enum LockType
    {
        Hard,
        Key
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _card = GetComponentInParent<Card>();
        if (_card == null)
        {
            Debug.LogError("Lock cannot get card", this);
        }
        
    }

    private void Start()
    {
        _card.BlockCard();

        _spriteRenderer.sprite = lockType switch
        {
            LockType.Hard => hardLockSprite,
            LockType.Key => keyLockSprite,
            _ => _spriteRenderer.sprite
        };
    }

    [ContextMenu("bruh")]
    public void Unlock()
    {
        if (lockType != LockType.Key) return;
        Instantiate(unlockEffect, transform.position, Quaternion.identity);
        _card.UnblockCard();
        gameObject.SetActive(false);
    }
}

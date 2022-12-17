using UnityEngine;

public class Lock : MonoBehaviour
{
    public Sprite keyLockSprite;
    public Sprite absoluteLockSprite;
    public GameObject unlockEffect;

    private SpriteRenderer _spriteRenderer;
    private Card _card;
    private bool _cardLocked;
    private SoundPlayer _soundPlayer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _card = GetComponentInParent<Card>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
    }
    void Update()
    {
        switch (_card.lockType)
        {
            case Card.CardLockType.Unlock:
                _spriteRenderer.enabled = false;
                if (_cardLocked)
                {
                    Instantiate(unlockEffect, transform.position, Quaternion.identity);
                    _soundPlayer.PlayRandom();
                }
                _cardLocked = false;
                break;
            case Card.CardLockType.KeyLock:
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = keyLockSprite;
                _cardLocked = true;
                break;
            case Card.CardLockType.AbsoluteLock:
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = absoluteLockSprite;
                _cardLocked = true;
                break;
        }
    }
}

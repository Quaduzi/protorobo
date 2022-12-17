using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject projectile;
    public SoundPlayer shotSoundPlayer;
    public SoundPlayer chargeSoundPlayer;
    public float animationSpeed = 1;

    private Animator _animator;
    private Card _card;
    private bool _active;
    private bool _gameStarted;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _card = GetComponentInParent<Card>();

        EventManager.OnCardBeginDrag.AddListener(PauseCannon);
        EventManager.OnCardEndDrag.AddListener(ProceedCannon);
        EventManager.OnGameStarted.AddListener(StartCannonLogic);
        
        PauseCannon();
    }
    
    void Update()
    {
        if (!_gameStarted) return;
        if (_active) return;
        if (_card.lockType == Card.CardLockType.KeyLock) return;
        _active = true;
        ProceedCannon();
    }

    private void PauseCannon()
    {
        _animator.speed = 0;
    }

    private void ProceedCannon()
    {
        if (!_active) return;
        _animator.speed = animationSpeed;
    }

    private void StartCannonLogic()
    {
        _gameStarted = true;
    }

    public void Shot()
    {
        shotSoundPlayer.PlayRandom();
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }

    public void Charge()
    {
        chargeSoundPlayer.PlayRandom();
    }
}

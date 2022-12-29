using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject projectile;
    //public SoundPlayer shotSoundPlayer;
    //public SoundPlayer chargeSoundPlayer;
    [SerializeField] private float animationSpeed = 1;

    private Animator _animator;
    private Card _card;
    private bool _active;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _card = GetComponentInParent<Card>();
        
        PauseCannon();
    }

    private void FixedUpdate()
    {
        if (_active) return;
        if (_card.IsLocked) return;
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

    public void Shot()
    {
        //shotSoundPlayer.PlayRandom();
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }

    public void Charge()
    {
        //chargeSoundPlayer.PlayRandom();
    }
}

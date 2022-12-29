using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Card _parentCard;
    private SpriteRenderer _renderer;
    private static readonly int VelocityY = Animator.StringToHash("VelocityY");

    [SerializeField] private float speed = 1;
    [SerializeField] private Direction direction = Direction.Right;
    [SerializeField] private SoundPlayer deathSoundPlayer;
    [SerializeField] private SoundPlayer flipSoundPlayer;

    public enum Direction
    {
        Right = 1,
        Left = -1
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _parentCard = GetComponentInParent<Card>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(speed * (int)direction, _rb.velocity.y);
        _animator.SetFloat(VelocityY, _rb.velocity.y);
    }

    private void ChangeParentCard(Transform newParent)
    {
        _parentCard.DisableSpriteMask();
        transform.SetParent(newParent);
        _parentCard = GetComponentInParent<Card>();
        _parentCard.EnableSpriteMask();
        
    }

    private void Flip()
    {
        flipSoundPlayer.PlayRandom();
        direction = (Direction)(-(int)direction);
        var localScale = transform.localScale;
        localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.TryGetComponent<Card>(out _)) ChangeParentCard(col.transform);
        if (col.TryGetComponent<FlipTrigger>(out _)) Flip();
        if (col.TryGetComponent<DeathTrigger>(out _)) StartCoroutine(Death());
    }
    
    private IEnumerator Death()
    {
        GlobalEventManager.SendDeath();
        deathSoundPlayer.PlayRandom();
        yield return StartCoroutine(DeathAnimation());
        SceneControl.ReloadLevel();
    }

    private IEnumerator DeathAnimation()
    {
        _renderer.maskInteraction = SpriteMaskInteraction.None;
        
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -9);
        
        var up = new Vector3(transform.localPosition.x, transform.localPosition.y + 1.5f, transform.localPosition.z);
        while (transform.localPosition != up)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, up, Time.fixedDeltaTime * 3);
            yield return null;
        }
        var down = new Vector3(transform.localPosition.x, transform.localPosition.y - 5, transform.localPosition.z);
        while (transform.localPosition != down)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, down, Time.fixedDeltaTime * 7);
            yield return null;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Card _parentCard;

    [SerializeField] private float speed = 1;
    [SerializeField] private Direction direction = Direction.Right;

    public enum Direction
    {
        Right = 1,
        Left = -1
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _parentCard = GetComponentInParent<Card>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(speed * (int)direction, _rb.velocity.y);
    }

    private void ChangeParentCard(Transform newParent)
    {
        _parentCard.DisableSpriteMask();
        transform.SetParent(newParent);
        _parentCard = GetComponentInParent<Card>();
        _parentCard.EnableSpriteMask();
        
    }

    private void Flip() => direction = (Direction) (-(int)direction);

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.TryGetComponent<Card>(out _)) ChangeParentCard(col.transform);
        if (col.TryGetComponent<FlipTrigger>(out _)) Flip();
    }
}

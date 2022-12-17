using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;
    public Direction direction = Direction.Right;

    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _walkSound;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _walkSound = GetComponent<AudioSource>();

        EventManager.OnCardBeginDrag.AddListener(DisableSimulation);
        EventManager.OnCardBeginDrag.AddListener(PauseAnimation);

        EventManager.OnCardEndDrag.AddListener(EnableSimulation);
        EventManager.OnCardEndDrag.AddListener(ProceedAnimation);
        
        EventManager.OnGameStarted.AddListener(ProceedAnimation);
        EventManager.OnGameStarted.AddListener(EnableSimulation);
        
        PauseAnimation();
        DisableSimulation();
    }
    
    void Update()
    {
        _rb.velocity = new Vector2(speed * (int)direction, _rb.velocity.y);
        _animator.SetFloat("VelocityY", _rb.velocity.y);
    }

    public void Flip()
    {
        direction = (Direction)((int)direction * -1);
        var localScale = _rb.transform.localScale;
        localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
        _rb.transform.localScale = localScale;
    }

    public void PauseAnimation()
    {
        _walkSound.Pause();
        _animator.speed = 0;
    }

    public void ProceedAnimation()
    {
        _walkSound.Play();
        _animator.speed = 1;
    }
    
    public void EnableSimulation()
    {
        _rb.simulated = true;
    }
    
    public void DisableSimulation()
    {
        _rb.simulated = false;
    }

    public void EnableGravity()
    {
        _rb.isKinematic = false;
    }
    
    public void DisableGravity()
    {
        _rb.isKinematic = true;
    }

    public enum Direction : int
    {
        Left = -1,
        Right = 1
    }
    
}

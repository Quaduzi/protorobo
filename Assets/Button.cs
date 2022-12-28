using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Button : MonoBehaviour
{
    [SerializeField] private UnityEvent onPress = new();
    [SerializeField] private Sprite pressedButton;

    private SpriteRenderer _renderer;
    private bool _isPressed;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isPressed) return;
        if (col.TryGetComponent<Player>(out _))
        {
            _isPressed = true;
            _renderer.sprite = pressedButton;
            onPress?.Invoke();
        }
    }
}

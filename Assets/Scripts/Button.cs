using System;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public Sprite unpressedButton;
    public Sprite pressedButton;
    public UnityEvent onPressed;

    private bool _isPressed;
    private SoundPlayer _soundPlayer;

    private void Start()
    {
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (_isPressed) return;
        onPressed?.Invoke();
        GetComponent<SpriteRenderer>().sprite = pressedButton;
        _soundPlayer.PlayRandom();
        _isPressed = true;
    }
}

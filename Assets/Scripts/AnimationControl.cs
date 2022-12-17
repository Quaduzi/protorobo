using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        EventManager.OnGameStarted.AddListener(ProceedAnimation);
        EventManager.OnCardBeginDrag.AddListener(PauseAnimation);
        EventManager.OnCardEndDrag.AddListener(ProceedAnimation);
        PauseAnimation();
    }

    public void PauseAnimation()
    {
        _animator.speed = 0;
    }

    public void ProceedAnimation()
    {
        _animator.speed = 1;
    }
}

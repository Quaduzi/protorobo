using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Key : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollect = new();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out _)) Collect();
    }

    private void Collect()
    {
        onCollect?.Invoke();
        gameObject.SetActive(false);
    }
}

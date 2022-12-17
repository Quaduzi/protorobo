using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1;
    public GameObject breakEffect;
    
    private Rigidbody2D _rb;
    private SoundPlayer _soundPlayer;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * speed;
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
        
        EventManager.OnCardBeginDrag.AddListener(PauseProjectile);
        EventManager.OnCardEndDrag.AddListener(ProceedProjectile);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GravityTrigger")) return;
        if (col.CompareTag("GhostForProjectile")) return;
        if (col.CompareTag("Portal")) return;
        if (col.CompareTag("Projectile")) return;
        if (col.CompareTag("Retranslator"))
        {
            Destroy(gameObject);
            return;
        }
        if (col.CompareTag("CardTrigger") || col.CompareTag("GravityTrigger"))
        {
            if (col.transform != transform.parent) ChangeParent(col.transform);
            return;
        }

        if (col.CompareTag("Player"))
        {
            var player = col.GetComponent<PlayerTriggerLogic>();
            player.StartCoroutine(player.Death());
        }
        DestroyProjectile();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("CardTrigger")) return;
        if (col.collider.CompareTag("GravityTrigger")) return;
        if (col.collider.CompareTag("GhostForProjectile")) return;
        if (col.collider.CompareTag("Portal")) return;
        if (col.collider.CompareTag("Projectile")) return;
        if (col.collider.CompareTag("Retranslator"))
        {
            Destroy(gameObject);
            return;
        }
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        _soundPlayer.transform.SetParent(effect.transform);
        _soundPlayer.PlayRandom();
        Destroy(gameObject);
    }
    
    private void ChangeParent(Transform parent)
    {
        var projectile = _rb.transform;
        projectile.SetParent(parent, true);
    }

    private void PauseProjectile()
    {
        _rb.simulated = false;
    }

    private void ProceedProjectile()
    {
        _rb.simulated = true;
    }
    
}

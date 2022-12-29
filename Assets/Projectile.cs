using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject breakEffect;
    
    private Rigidbody2D _rb;
    //private SoundPlayer _soundPlayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_soundPlayer = GetComponentInChildren<SoundPlayer>();
    }

    private void Start()
    {
        _rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // (col.CompareTag("Retranslator"))
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        Debug.Log("Trigger " + col.name);
        if (col.TryGetComponent<Card>(out var card))
        {
            transform.SetParent(card.transform, true);
            return;
        }
        
        if (col.TryGetComponent<TransitionTrigger>(out var trans))
        {
            transform.SetParent(trans.transform, true);
            return;
        }
        
        if (TryGetComponent<Key>(out _)) return;
        if (TryGetComponent<Portal>(out _)) return;
        if (TryGetComponent<Button>(out _)) return;
        
        DestroyProjectile();
    }
    

    private void DestroyProjectile()
    {
        var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        //_soundPlayer.transform.SetParent(effect.transform);
        //_soundPlayer.PlayRandom();
        Destroy(gameObject);
    }

}

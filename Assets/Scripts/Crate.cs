using UnityEngine;

public class Crate : MonoBehaviour
{
    public int health = 2;
    public Sprite brokenSprite;
    public GameObject breakEffect;

    private SpriteRenderer _renderer;
    private bool _lowHP;
    private SoundPlayer _soundPlayer;
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
    }
    void Update()
    {
        if (health == 1 & !_lowHP)
        {
            _renderer.sprite = brokenSprite;
            _lowHP = true;
        }

        if (health <= 0)
        {
            BreakCrate();
        }
    }

    public void BreakCrate()
    {
        var effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        _soundPlayer.transform.SetParent(effect.transform);
        _soundPlayer.PlayRandom();
        gameObject.SetActive(false);
    }

    public void DealDamage()
    {
        health--;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            DealDamage();
        }
    }
}

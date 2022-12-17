using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public bool connected;

    private Collider2D _myCollider2D;

    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _myCollider2D = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        
    }

    private bool CheckConnected()
    {
        var filter = new ContactFilter2D
        {
            useTriggers = true
        };
        var colliders = new List<Collider2D>();
        Debug.Log(_myCollider2D.OverlapCollider(filter, colliders));
        colliders.ForEach(x => Debug.Log(x.tag));
        
        var cardsCount = colliders.Count(x => x.CompareTag("CardTrigger"));
        Debug.Log(cardsCount);
        return cardsCount == 2;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && CheckConnected())
        {
            Debug.Log("Player Enter");
            _animator.SetTrigger("TransitionTriggerEnter");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("TransitionTriggerExit");
        }
    }
}

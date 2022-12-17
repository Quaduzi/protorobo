using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRetranslator : MonoBehaviour
{
    public Transform shotPoint;
    public GameObject projectile;
    void Start()
    {
        
    }
    
    public void Shot()
    {
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            Shot();
        }
    }
}

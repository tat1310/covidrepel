using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject DestroyEffect;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisiongameobject = collision.gameObject;
        if(collisiongameobject.name != "Player")
        {
            Die();
        }
    }
    private void Die()
    {
        if(DestroyEffect != null)
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

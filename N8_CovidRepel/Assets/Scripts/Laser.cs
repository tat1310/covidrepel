using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject DestroyEffect;
    public GameObject DestroyCovidEffect;
    public GameObject DestroyBossEffect;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisiongameobject = collision.gameObject;
        if(collisiongameobject.name != "Character")
        {
            Die();
        }
        if (collision.collider.tag == "Covid")
        {
            if (DestroyCovidEffect != null)
            {
                Instantiate(DestroyCovidEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "KingCovid")
        {
            KingCovid kingcv = collision.collider.GetComponent<KingCovid>();
            if (kingcv != null)
            {
                kingcv.ChangeHealth(-1);
            }
            if(kingcv.health < 1)
            {
                if (DestroyBossEffect != null)
                {
                    Instantiate(DestroyBossEffect, transform.position, Quaternion.identity);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "destroylaser")
        {
            Destroy(gameObject);
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

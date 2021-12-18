using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCovid : MonoBehaviour
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
        if (collisiongameobject.name != "Player")
        {
            Character character = collision.collider.GetComponent<Character>();
            if (character != null)
            {
                character.ChangeHealth(-1);
            }
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "destroylasercovid")
        {
            Die2();
        }
    }
    private void Die()
    {
        if (DestroyEffect != null )
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    private void Die2()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidGunner : MonoBehaviour
{
    public float shootSpeed = 10, shootTimer = 0.2f;
    public GameObject laser;

    public Transform shootPos;
    private bool isShooting = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        isShooting = false;
        GameObject newlaser = Instantiate(laser, shootPos.position, Quaternion.identity);
        newlaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed, 0f);

        yield return new WaitForSeconds(shootTimer);
        isShooting = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Character character = collision.collider.GetComponent<Character>();
            if (character != null)
            {
                character.ChangeHealth(-1);
            }
        }
    }
}

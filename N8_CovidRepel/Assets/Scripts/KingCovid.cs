using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCovid : MonoBehaviour
{
    public float VanTocVat;
    public bool DiChuyenLen = true;

    public GameObject door;
    public GameObject laser;
    public float timeFire = 1f;
    float fireRate;
    float nextFire;

    public int maxHealth = 50;
    int currentHealth;
    public int health { get { return currentHealth; } }

    void Start()
    {
        fireRate = timeFire;
        nextFire = Time.time;
        currentHealth = maxHealth;
    }
    void Update()
    {
        timeToFire();
        if(currentHealth < 1)
        {
            Destroy(gameObject);
            door.SetActive(false);
        }
    }
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("HP boss" + currentHealth.ToString() +" / " + maxHealth.ToString());
    }
    void timeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(laser, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
    void FixedUpdate()
    {
        Vector2 DiChuyen = transform.localPosition;
        if (DiChuyenLen)
        {
            DiChuyen.y -= VanTocVat * Time.deltaTime;
        }
        else
        {
            DiChuyen.y += VanTocVat * Time.deltaTime;
        }
        transform.localPosition = DiChuyen;
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "maxtren")
        {
            QuayMat();
        }
        if (collision.tag == "maxduoi")
        {
            QuayMat();
        }
    }
    void QuayMat()
    {
        DiChuyenLen = !DiChuyenLen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    public GameObject laserchange;
    void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();

        if (other.tag == "Player" && character != null)
        {
            character.ChangeLaser(laserchange);
            Destroy(gameObject);
        }
    }
}

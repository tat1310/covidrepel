using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Character character = other.GetComponent<Character>();

        if (other.tag == "Player" && character != null)
        {
            if (character.health < character.maxHealth)
            {
                character.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}

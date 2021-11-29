using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            collision.gameObject.transform.SetParent(transform);
            Debug.Log("Stay");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Character")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

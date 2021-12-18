using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public GameObject losePanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Chet roi");
            Time.timeScale = 0f;
            GamePlayController.instance.ShowLosePanel();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLaser : MonoBehaviour
{
    private SpriteRenderer rend;
    public GameObject laser1;
    Character cr;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cr.laser = laser1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float moveSpeed = 5f;
    public float forcejump = 700f;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    public float shootSpeed = 10, shootTimer = 0.2f;
    public GameObject laser;
    public GameObject lasernew;

    public Transform shootPos;
    private bool isShooting = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (isShooting)
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetMouseButtonDown(0))
        {
            laser = lasernew;
        }
    }
    private void Move()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * forcejump);
        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("IsRunning", true);
        else
            anim.SetBool("IsRunning", false);
        if (rb.velocity.y == 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
        }
        if (rb.velocity.y > 0)
        {
            anim.SetBool("IsJumping", true);
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
    }

    IEnumerator Shoot()
    {
        isShooting = false;
        GameObject newlaser = Instantiate(laser, shootPos.position, Quaternion.identity);
        newlaser.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed  * localScale.x, 0f);

        yield return new WaitForSeconds(shootTimer);
        isShooting = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }
    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }
}

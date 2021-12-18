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

    public Transform shootPos;
    private bool isShooting = true;

    public int maxHealth = 5;
    public float timeInvincible = 2;
    public int health { get { return currentHealth; }}
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    public ParticleSystem protectiveEffect;
    private int protective = 2;
    public float timeProtective = 5;
    float protectiveTimer;

    public AudioSource jumpSound;
    public AudioSource changeLaserSound;
    public AudioSource itemSound;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (isShooting)
        {
            StartCoroutine(Shoot());
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (protective == 1)
        {
            protectiveEffect.Play();
            protectiveTimer -= Time.deltaTime;
            if (protectiveTimer < 0)
                protective = 3;
        }
        if(protective == 3 && protectiveEffect.isPlaying)
        {
            protectiveEffect.Clear();
            Debug.Log("Stop effect");
        }
        if(currentHealth == 0)
        {
            Debug.Log("Chet roi");
            Time.timeScale = 0f;
            GamePlayController.instance.ShowLosePanel();
        }
    }
    private void Move()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * forcejump);
            jumpSound.Play();
        }
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
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible || protective == 1)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    public void ChangeLaser(GameObject newlaser)
    {
        laser = newlaser;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "mask")
        {
            itemSound.Play();
            protective = 1;
            protectiveTimer = timeProtective;
        }
        if (other.tag == "gel")
        {
            changeLaserSound.Play();
        }
        if (other.tag == "medicine")
        {
            itemSound.Play();
        }
    }
}

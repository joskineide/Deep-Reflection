using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipControler : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float curSX, curSY, maxS;
    public float nMaxS, focusS, sDownSpd, invFrames, maxInvFrames;
    public int health;
    public GameObject gameOver, healthGUI;
    private SpriteRenderer sprite;
    public AudioSource aSource;
    public AudioClip aClipTakeDamage, aClipDie;

    private Animator anim;
    

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        gameOver.SetActive(false);
        invFrames = maxInvFrames;
    }

    public void TakeDamage() 
    {
        aSource.PlayOneShot(aClipTakeDamage);
        health--;
        invFrames = 0;
    }

    void Update()
    {
        anim.SetFloat("Speed",rb2d.velocity.magnitude);
        anim.SetInteger("Health", health);


        healthGUI.transform.localScale = new Vector2(health / 5f, 1);

        
        if (invFrames >= maxInvFrames)
        {
            sprite.color = new Color(255, 255, 255, 1);

        }
        else if (health > 0)
        {
            sprite.color = new Color(255, 255, 255, 0.3f);
        }
        if (health > 0)
        {
            if (invFrames < maxInvFrames)
            {
                invFrames += Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxS = focusS;
                anim.SetBool("inFocus", true);
            }
            else
            {
                maxS = nMaxS;
                anim.SetBool("inFocus", false);
            }
            rb2d.velocity = new Vector2(curSX, curSY);
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                    curSX = maxS * Mathf.Sqrt(2) / 2;
                else if (Input.GetKey(KeyCode.S))
                    curSX = maxS * Mathf.Sqrt(2) / 2;
                else
                    curSX = maxS;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W))
                    curSX = -maxS * Mathf.Sqrt(2) / 2;
                else if (Input.GetKey(KeyCode.S))
                    curSX = -maxS * Mathf.Sqrt(2) / 2;
                else
                    curSX = -maxS;
            }
            else
            {
                if (curSX > 0.5 || curSX < -0.5)
                {
                    curSX = curSX / sDownSpd;
                }
                else
                {
                    curSX = 0;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A))
                    curSY = maxS * Mathf.Sqrt(2) / 2;
                else if (Input.GetKey(KeyCode.D))
                    curSY = maxS * Mathf.Sqrt(2) / 2;
                else
                    curSY = maxS;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                    curSY = -maxS * Mathf.Sqrt(2) / 2;
                else if (Input.GetKey(KeyCode.D))
                    curSY = -maxS * Mathf.Sqrt(2) / 2;
                else
                    curSY = -maxS;
            }
            else
            {
                if (curSY > 0.5 || curSY < -0.5)
                {
                    curSY = curSY / sDownSpd;
                }
                else
                {
                    curSY = 0;
                }
            }

        }
        else
        {
            gameOver.SetActive(true);
            rb2d.velocity = new Vector2(0, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            { SceneManager.LoadScene(2); }
        }
    }

  
}

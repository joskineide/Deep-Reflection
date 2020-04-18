using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public ShipControler target;
    public float fireRate, moveSpeed;
    private float angle, firingCd,takeDamageTimer, trueAngle;
    public GameObject bullet, fireFrom;
    public int health, shootingType,shipID;
    public GManager gManager;
    private AudioSource aSource, gmASource;
    public AudioClip aClipShoot, aClipDestroy, aClipTakeDamage;
    public GameObject deathParticle;
    private Animator anim;
    private SpriteRenderer sprite;               
	void Start () {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        aSource = GetComponent<AudioSource>();
        gManager = FindObjectOfType<GManager>();
        gmASource = gManager.GetComponent<AudioSource>();
        firingCd = fireRate+2;
        target = FindObjectOfType<ShipControler>();
        Debug.Log("Spawned ship: "+shipID);
        anim.SetInteger("ID", shipID);
    }

	void Update () {
        if (takeDamageTimer > 0)
        {
            takeDamageTimer -= Time.deltaTime;
            sprite.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            sprite.color = new Color(255, 255, 255, 1);
        }
        if (target.health > 0)
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            if (firingCd > 0)
            {
                firingCd -= Time.deltaTime;
            }
            else
            {
                switch (shootingType)
                {
                    case 0:
                        Instantiate(bullet, fireFrom.transform.position, transform.rotation);
                        break;
                    case 1:
                        Instantiate(bullet, fireFrom.transform.position, Quaternion.Euler(0, 0, trueAngle + 5));
                        Instantiate(bullet, fireFrom.transform.position, Quaternion.Euler(0, 0, trueAngle - 5));
                        break;
                    case 2:
                        Instantiate(bullet, fireFrom.transform.position, Quaternion.Euler(0, 0, trueAngle));
                        Instantiate(bullet, fireFrom.transform.position, Quaternion.Euler(0, 0, trueAngle + 30));
                        Instantiate(bullet, fireFrom.transform.position, Quaternion.Euler(0, 0, trueAngle - 30));
                        break;
                }
                aSource.PlayOneShot(aClipShoot);
                firingCd = fireRate;
            }
        }
        angle = Mathf.Atan((target.transform.position.y - transform.position.y) /
        (target.transform.position.x - transform.position.x)) * (180/Mathf.PI);
        if (target.transform.position.x > transform.position.x)
        {
            trueAngle = angle - 90;
        }
        else
        {
            trueAngle = angle - 270;
        }
        transform.rotation = Quaternion.Euler(0, 0, trueAngle);
        if (health <= 0)
        {
            Instantiate(deathParticle,transform.position,Quaternion.identity);
            gmASource.PlayOneShot(aClipDestroy);
            gManager.activeEnemies--;
            gManager.score += 100;
            Destroy(gameObject);
        }
    }
    public void TakeDamage()

    {
        aSource.PlayOneShot(aClipTakeDamage);
        takeDamageTimer = 0.3f;
        health--;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ShipControler ship = other.GetComponent<ShipControler>();
            if (ship.invFrames >= ship.maxInvFrames)
            {
                ship.TakeDamage();
                ship.health = 0;
            }
        }
    }
}

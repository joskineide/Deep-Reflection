using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float curRot, baseSpeed, lifeSpawn;
    public bool hasReflected;
    public GameObject parryParticle;
    private Animator anim;
    public GManager gManager;
    
    void Start()
    {
        gManager = FindObjectOfType<GManager>();
        anim = GetComponent<Animator>();
        curRot = transform.eulerAngles.z;
    }
    void Update()
    {
        anim.SetBool("reflected", hasReflected);
        transform.Translate(0, baseSpeed * Time.deltaTime, 0);
        lifeSpawn+= Time.deltaTime;
        if (lifeSpawn > 5)
        { Destroy(gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit "+other.name);

        if (other.gameObject.tag == "shield" && !hasReflected)
        {
            ShieldScript shield = other.GetComponent<ShieldScript>();
            if (shield.reflecting)
            {
                
                transform.Rotate(0, 0, (other.transform.eulerAngles.z - transform.eulerAngles.z));
                Instantiate(parryParticle, transform.position, transform.rotation);
                hasReflected = true;
                shield.hasReflected = hasReflected;
                shield.isRecharging = false;
                shield.reflecting = false;
                if (shield.shieldHealth < shield.maxShieldHealth) { shield.shieldHealth++; }
                shield.rechargeTime = shield.maxRechargeTimer;
                shield.activeReflectTimer = shield.maxTimeReflecting;
                shield.aSource.PlayOneShot(shield.aClipReflect);
            }
            else if (!shield.isRecharging)
            {
                shield.aSource.PlayOneShot(shield.aClipBlock);
                shield.shieldHealth--;
                Destroy(gameObject);
            }
        }
        
        if (other.gameObject.tag == "Player")
        {
            ShipControler ship = other.GetComponent<ShipControler>();
            if (ship.invFrames >= ship.maxInvFrames)
            {
                ship.TakeDamage();

                Destroy(gameObject);
            }
        }
        if (lifeSpawn > 0.1f && other.gameObject.tag == "enemy")
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (!hasReflected)
            {
                enemy.TakeDamage();
                gManager.score += 50;
            }
            else
            {
                
                enemy.health -= 3;
                gManager.score += 200;
            } 
            Destroy(gameObject);
        }
    }
}


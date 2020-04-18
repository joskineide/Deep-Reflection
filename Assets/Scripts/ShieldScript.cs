using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {
    public float rotateS, curRot, rechargeTime, activeReflectTimer; 
    public float focusS, nRotS,maxRechargeTimer, maxTimeReflecting;
    public bool reflecting, isRecharging, canBreakSound;
    public int maxShieldHealth, shieldHealth;
    public SpriteRenderer sprite;
    public AudioSource aSource;
    public AudioClip aClipBlock, aClipReflect, aClipBreak;
    public GameObject shieldHealthGUI;

    public bool hasReflected;

    private Animator anim;
  

    void Start()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        shieldHealth = maxShieldHealth;
        activeReflectTimer = maxTimeReflecting;
        rechargeTime = maxRechargeTimer;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        //Animations
        anim.SetBool("Reflecting",reflecting);
        anim.SetBool("recharging", isRecharging);
        anim.SetFloat("activeTime",activeReflectTimer);
        anim.SetInteger("ShieldHealth", shieldHealth);

        lookAtMouse();
        if (!reflecting)
        {
            if (!isRecharging)
            {
                shieldHealthGUI.transform.localScale = new Vector2(shieldHealth / 4f, 1);
            }
            if (canBreakSound && isRecharging)
            {
                aSource.PlayOneShot(aClipBreak); canBreakSound = false;
            }
            if (rechargeTime < maxRechargeTimer)
            {
                rechargeTime += Time.deltaTime;
                canBreakSound = false;
            }
            else
            {
                isRecharging = false;
                canBreakSound = true;
            }
        }
       /* if (curRot > 360) curRot -= 360;
        else if (curRot < 0) curRot += 360;
       */
        if (shieldHealth <= 0)
        {
            shieldHealth = maxShieldHealth;
            rechargeTime = -maxRechargeTimer;
            isRecharging = true;
            aSource.PlayOneShot(aClipBreak);
        }
        if (activeReflectTimer < maxTimeReflecting)
        {
            activeReflectTimer += Time.deltaTime;
        }
        else 
        {
            reflecting = false;
         }
        if (Input.GetButtonDown("Fire1") && !isRecharging)
        {
            activeReflectTimer = 0;
            rechargeTime = 0;
            reflecting = true;
            isRecharging = true;
        }
        /*if (Input.GetKey(KeyCode.LeftShift)) { rotateS = focusS; }
        else { rotateS = nRotS; }
        if (Input.GetKey(KeyCode.RightArrow)) { curRot -= rotateS; }
        else if (Input.GetKey(KeyCode.LeftArrow)) { curRot += rotateS; }
        transform.rotation = Quaternion.Euler(0, 0, curRot);   */
	}

    void lookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
    }
}

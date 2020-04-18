using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float xInitial;
    public float xLimit;
    public float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-xSpeed * Time.deltaTime,0,0));

        if(transform.position.x < xLimit)
        {
            transform.position = new Vector3(xInitial,transform.position.y,transform.position.z);            
        }
    }
}

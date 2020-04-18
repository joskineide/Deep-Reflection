using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{

    ParticleSystem part;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= part.startLifetime)
        {
            Destroy(gameObject);
        }
    }
}

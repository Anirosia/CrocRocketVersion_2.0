using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    public GameObject PlayerObj;
    private PersonController PlayerRef;

    public ParticleSystem leftFlame;
    public ParticleSystem rightFlame;
    public Light glow;
    AudioSource sound;
    AudioClip clip;

    void Start()
    {
        PlayerRef = PlayerObj.GetComponent<PersonController>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (PlayerRef.doubleJumped)
        {
            sound.Play();
            leftFlame.Play();
            rightFlame.Play();
            glow.enabled = true;


        }
        else
        {
            leftFlame.Stop();
            rightFlame.Stop();
            glow.enabled = false;
        }
    }
}

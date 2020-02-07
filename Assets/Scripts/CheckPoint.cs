using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject RespawnArea; //the area the point moves to
    public GameObject Respawn; //the point that moves


    private void Start()
    {
        Respawn = GameObject.FindGameObjectWithTag("Respawn");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Respawn.transform.localPosition = RespawnArea.transform.localPosition;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject Player;
    public GameObject Target;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.transform.position = Target.transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject Player;
    public GameObject Respawn;

    private void OnTriggerEnter(Collider other)
    {
        respawnPlayer();
    }


    public void respawnPlayer()
    {
        Player.transform.localPosition = Respawn.transform.localPosition;
    }
}

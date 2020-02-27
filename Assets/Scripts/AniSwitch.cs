using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniSwitch : MonoBehaviour
{
    public bool offScreen;
    public bool onScreen;

    public void nowOffScreen()
    {        
        offScreen = true;   
    }

    public void nowOnScreen()
    {
        onScreen = true;
    }

}

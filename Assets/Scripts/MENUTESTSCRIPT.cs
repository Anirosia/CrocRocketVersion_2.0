using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENUTESTSCRIPT : MonoBehaviour
{

    public GameObject Movable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movable.transform.Translate(10.0f, 0.0f, 0.0f);
    }
}

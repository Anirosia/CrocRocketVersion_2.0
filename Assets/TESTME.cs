using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTME : MonoBehaviour
{

    public GameObject[] slots;
    ArrayList items = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject slot in slots)
        {
            items.Add(slot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(items[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    float randomNumber = Random.Range(1, 5);
    public Transform farEnd;
    private Vector3 frometh;
    private Vector3 untoeth;
    private float secondsForOneLength;

    void Start()
    {
        frometh = transform.position;
        untoeth = farEnd.position;
        secondsForOneLength = randomNumber;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(frometh, untoeth,
         Mathf.SmoothStep(0f, 1f,
          Mathf.PingPong(Time.time / secondsForOneLength, 1f)
        ));
    }
}

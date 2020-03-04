using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public Transform farEnd;
    private Vector3 frometh;
    private Vector3 untoeth;
    private float secondsForOneLength = 1f;
    public float speed;

    void Start()
    {
        frometh = transform.position;
        untoeth = farEnd.position;
        speed = Random.Range(0.5f, 2);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(frometh, untoeth,
         Mathf.SmoothStep(0f, 1f,
          Mathf.PingPong(Time.time / secondsForOneLength * speed, 1f)
        ));
    }
}

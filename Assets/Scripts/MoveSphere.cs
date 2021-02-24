using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;// MetersPerFrame;
    public float distanceInMeters;
    private float speedMetersPerSec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 1cup flour, 1 tsp salt, 2/3 milk, 2 eggs
    }

    // Update is called once per frame
    void Update()
    {
        //speedMetersPerSec = speedMetersPerFrame * Time.deltaTime;

        float mH = Input.GetAxis("Horizontal") * speed;
        float mV = Input.GetAxis("Vertical") * speed;

        //mH *= Time.deltaTime;
        //mV *= Time.deltaTime;

        //transform.Translate(mH, 0, mV);

        rb.velocity = new Vector3(mH * speed, rb.velocity.y, mV * speed);
    }
}

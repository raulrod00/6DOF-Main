using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{

    private Rigidbody rb;
    public float peakVel;    // MetersPerSecond;
    public float timeLength;
    private float startTime;
    private bool flag;
    private float halfTime;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        rb = GetComponent<Rigidbody>();
        halfTime = timeLength / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") & !flag)
        {
            flag = true;
            if (flag)
            {
                StartCoroutine(MoverFwd());
            }
        }

        if (Input.GetKeyDown("m"))
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 0.01f);
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 4.0f);
        }

        if (Input.GetKeyDown("n"))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z + 0.01f);
        }

        if (Input.GetKeyDown("b"))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z - 0.01f);
        }
    }

    private IEnumerator MoverFwd()
    {
        
        float elapsedTime = 0;

        while (elapsedTime < halfTime)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.SmoothStep(0, peakVel, elapsedTime / halfTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, peakVel);
        yield return StartCoroutine(MoveBkd());
    }

    private IEnumerator MoveBkd()
    {
        float elapsedTime = 0;

        elapsedTime = 0;

        while (elapsedTime < halfTime)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.SmoothStep(peakVel, 0, elapsedTime / halfTime));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        flag = false;
    }
}

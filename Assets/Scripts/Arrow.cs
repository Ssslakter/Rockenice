using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    public float lifeTimeAfterHit = 1;
    public GameObject spark, trail;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = false;
    }
    void Update()
    {
        if (transform.parent == null && rb != null)
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
    public void SetSpeed(Vector3 startVelocity)
    {
        GameObject newTrail = Instantiate(trail, transform);
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.velocity = startVelocity;  
    }

    private void OnCollisionEnter()
    {
        Destroy(rb);
        Destroy(gameObject, lifeTimeAfterHit);
        GameObject newSpark = Instantiate(spark, transform.position, transform.rotation);
        newSpark.transform.parent = transform;
    }

}

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

    //private void FixedUpdate()
    //{
    //    GroundCheck();
    //}
    public void SetSpeed(Vector3 startVelocity)
    {
        GameObject newTrail = Instantiate(trail, transform);
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.velocity = startVelocity;  
    }
    //private void GroundCheck()
    //{
    //    RaycastHit hitInfo;
    //    if (Physics.SphereCast(transform.position, rb, Vector3.down, out hitInfo,
    //                           ((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
    //    {
    //        Destroy(rb);
    //        Destroy(gameObject, lifeTimeAfterHit);
    //        GameObject newSpark = Instantiate(spark, transform.position, transform.rotation);
    //        newSpark.transform.parent = transform;

    //    }


    //}


    private void OnCollisionEnter()
    {

        Destroy(rb);
        Destroy(gameObject, lifeTimeAfterHit);
        GameObject newSpark = Instantiate(spark, transform.position, transform.rotation);
        newSpark.transform.parent = transform;
    }

}

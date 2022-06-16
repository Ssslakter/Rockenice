using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNode : MonoBehaviour
{
    public GameObject node;
    public GameObject childNode;

    private Transform cylinder;
    private Vector3 scaleNode;
    private Vector3 scaleCylinder;

    // Start is called before the first frame update
    void Start()
    {
        cylinder = node.transform.GetChild(0);
        var parentValues = node.GetComponentInParent<RopeController>();
        //node.transform.localScale = parentValues.scaleNode;
        //scaleCylinder = parentValues.scaleCylinder;

        node.GetComponent<Rigidbody>().mass = parentValues.mass;
        scaleCylinder = parentValues.scaleCylinder;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (childNode != null)
        {
            Vector3 direction = childNode.transform.position - node.transform.position;

            cylinder.localPosition = Vector3.down * direction.magnitude / 2f;
            cylinder.localRotation = Quaternion.LookRotation(Vector3.forward, direction);
            cylinder.localScale = new Vector3(scaleCylinder.x, direction.magnitude / 2f, scaleCylinder.z);
        }
    }
}

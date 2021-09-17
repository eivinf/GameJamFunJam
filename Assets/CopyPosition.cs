using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform target;
    public float force;

    void FixedUpdate()
    {
        //GetComponent<Rigidbody>().AddForce((target.position - transform.position) * force);
        transform.position = target.position;
        GetComponent<Rigidbody>().velocity = target.GetComponent<Rigidbody>().velocity;
    }
}

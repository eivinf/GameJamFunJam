using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform target;
    public float force;
    public bool isSwinging;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging) {
            target.position = transform.position;
            isSwinging = true;
            target.GetComponent<Rigidbody>().velocity = Vector3.zero;
         }
        if (Input.GetMouseButtonUp(0))
        {
            isSwinging = false;
        }
        if (isSwinging)
        {
            //GetComponent<Rigidbody>().AddForce((target.position - transform.position) * force);
            transform.position = target.position;
            GetComponent<Rigidbody>().velocity = target.GetComponent<Rigidbody>().velocity;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public Transform target;
    public Transform anchor;
    public float force;
    public bool isSwinging;
    public Transform camera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging) {

            var hit = new RaycastHit();
            if (Physics.Raycast(camera.position, camera.forward, out hit, 1000))
            {
                anchor.position = hit.point;
                target.position = transform.position;
                isSwinging = true;
                target.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
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

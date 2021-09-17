using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnchorToTargetPosition : MonoBehaviour
{

    public Transform target;
    
    void FixedUpdate()
    {
        GetComponent<HingeJoint>().anchor = Quaternion.Inverse(transform.rotation)* (target.position - transform.position);
    }
}

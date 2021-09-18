using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVisulize : MonoBehaviour
{
    public Transform[] nodes = new Transform[16];
    public Vector3[] positions = new Vector3[16];
    public Transform end;
    public Transform endTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        end.position = endTarget.position;
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].position = positions[i];
            if (i > 1)
            {
                nodes[i].rotation = Quaternion.FromToRotation(Vector3.forward, positions[i-1] - positions[i]);
            }
        }
    }
}

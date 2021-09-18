using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeVisulize : MonoBehaviour
{
    public Transform[] nodes = new Transform[16];
    public Vector3[] positions = new Vector3[16];
    public Transform endNode;
    public Transform player;
    public Rope rope;
    public RopeDangling ropeController;
    public Vector3 anchorPosition = Vector3.zero;
    public void Attatch(Vector3 anchor, Transform player)
    {
        this.player = player;
        anchorPosition = anchor;
        rope = new Rope(anchorPosition, player.position);
        ropeController = new RopeDangling();
    }

    public void ReelIn(float reelFactor)
    {
        rope.ReelIn(reelFactor);
    }
    public void Detatch()
    {
        ropeController.attatched = false;
        player = nodes[15];
    }

    // Update is called once per frame
    void Update()
    {
        if (!(ropeController is null))
        {
            Debug.Log(1);
            ropeController.GetNewNodePositions(rope, player.position);
            positions = rope.GetPositions();
            endNode.position = player.position;
            endNode.rotation = Quaternion.FromToRotation(Vector3.back, positions[15] - endNode.position);
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].position = positions[i];
                if (i > 1)
                {
                    nodes[i].rotation = Quaternion.FromToRotation(Vector3.back, positions[i - 1] - positions[i]);
                }
            }
        }
        
    }
}

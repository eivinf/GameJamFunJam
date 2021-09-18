using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Script based on video by Sebastian Lague
    public const float gravity = 9.81f; 
    public const int numIterations = 10;
    public const int nodeCount = 16;

    public class Point
    {
        public Vector3 position, prevPosition;
        public bool locked;
    }
    public class Stick
    {
        public Point pointA, pointB;
        public float length;
    }
    public class Rope
    {
        public Point anchorPoint;
        public Point endPoint;
        public Point[] points;
        public Stick[] sticks;
        public float ropeLength;
    }

    public Rope MakeNewRope(Vector3 anchorPosition, Vector3 endPosition)
    {
        return null;
    }

    public void GetNewNodePositions(Rope rope)
    {
        foreach (Point point in rope.points)
        {
            if (!point.locked)
            {
                Vector3 positionBeforeUpdate = point.position;
                point.position += point.position - point.prevPosition;
                point.position += Vector3.down * gravity * Time.deltaTime * Time.deltaTime;
                point.prevPosition = positionBeforeUpdate;
            }
        }
        for (int i = 0; i < numIterations; i++)
        {
            foreach (Stick stick in rope.sticks)
            {
                Vector3 stickCentre = (stick.pointA.position + stick.pointB.position) / 2;
                Vector3 stickDir = (stick.pointA.position - stick.pointB.position).normalized;
                if (!stick.pointA.locked)
                {
                    stick.pointA.position = stickCentre + stickDir * stick.length / 2;
                }
                if (!stick.pointB.locked)
                {
                    stick.pointB.position = stickCentre - stickDir * stick.length / 2;
                }
            }
        }
    }

}

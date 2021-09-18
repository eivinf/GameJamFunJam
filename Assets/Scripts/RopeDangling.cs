using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Vector3 position, prevPosition;
    public bool locked;
    public Point(Vector3 position, bool locked)
    {
        this.position = position;
        prevPosition = position;
        this.locked = locked;
    }
}
public class Stick
{
    public Point pointA, pointB;
    public float length;
    public Stick(Point pointA, Point pointB)
    {
        length = Vector3.Distance(pointA.position, pointB.position) - 0.1f;
        this.pointA = pointA;
        this.pointB = pointB;
    }
}
public class Rope
{
    public Point anchorPoint;
    public Point endPoint;
    public Point[] points;
    public Stick[] sticks;
    public float ropeLength;
    public Rope(Vector3 anchorPosition, Vector3 endPosition)
    {
        anchorPoint = new Point(anchorPosition, true);
        points = new Point[RopeDangling.nodeCount];
        sticks = new Stick[RopeDangling.nodeCount - 1];
        points[0] = anchorPoint;
        for (int i = 1; i < RopeDangling.nodeCount; i++)
        {
            var pointPosition = anchorPosition + (endPosition - anchorPosition) * ((float)i / (RopeDangling.nodeCount - 1));
            var newPoint = new Point(pointPosition, false);
            points[i] = newPoint;
            sticks[i - 1] = new Stick(points[i - 1], points[i]);
        }
        this.endPoint = points[RopeDangling.nodeCount - 1];
    }
    public Vector3[] GetPositions()
    {
        var nodePositions = new Vector3[RopeDangling.nodeCount];
        for (int i = 0; i < RopeDangling.nodeCount; i++)
        {
            nodePositions[i] = points[i].position;
        }
        return nodePositions;
    }
    public void ReelIn(float reelFactor)
    {
        for (int i = 0; i < sticks.Length; i++)
        {
            sticks[i].length -= reelFactor / sticks.Length;
        }
    }
}

public class RopeDangling : MonoBehaviour
{
    // Script based on video by Sebastian Lague
    public const float gravity = 9.81f; 
    public const float drag = 0.005f;
    public const int numIterations = 2;
    public static int nodeCount = 16;
    public const float attraction = 200f;
    public bool attatched = true;

    public void GetNewNodePositions(Rope rope, Vector3 playerPosition)
    {
        if (attatched)
        {
            rope.points[nodeCount - 1].position += (playerPosition - rope.points[nodeCount - 1].position) * attraction * Time.deltaTime * Time.deltaTime;
        }
        else
        {
            rope.points[nodeCount - 1].position += Vector3.down * attraction * Time.deltaTime * Time.deltaTime;
        }
        foreach (Point point in rope.points)
        {
            if (!point.locked)
            {
                Vector3 positionBeforeUpdate = point.position;
                point.position += (point.position - point.prevPosition) * (1f - drag);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Script based on video by Sebastian Lague
    public Point[] points;
    public Stick[] sticks;
    private float gravity = 9.81f; 
    private int numIterations = 10;
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

    void Simulate()
    {
        foreach (Point point in points)
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
            foreach (Stick stick in sticks)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

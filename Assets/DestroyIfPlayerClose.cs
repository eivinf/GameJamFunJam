using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfPlayerClose : MonoBehaviour
{
    public float detectionDistance;
    public Transform Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.position, transform.position) < detectionDistance)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float verticalMovementSpeed;
    public float horizontalMovementSpeed;
    public Vector3 desireMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //GetComponent<Rigidbody>().velocity -= desireMove;
        var forwardMove = transform.rotation * Vector3.forward * verticalMovementSpeed * Input.GetAxis("Vertical");
        var rightMove = transform.rotation * Vector3.right * horizontalMovementSpeed * Input.GetAxis("Horizontal");

        desireMove = rightMove + forwardMove;
        transform.position += desireMove;
    }
}

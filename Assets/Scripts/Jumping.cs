using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumping : MonoBehaviour
{
    
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    public bool jumpLeft;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;

    }

    void OnCollisionExit()
    {
        isGrounded = false;
        jumpLeft = true;
    }

        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpLeft)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            jumpLeft = false;
        }
    }
    
}

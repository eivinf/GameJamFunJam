using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float verticalMovementSpeed;
    public float horizontalMovementSpeed;
    public float verticalSprintSpeed;
    public float horizontalSprintSpeed;
    public Vector3 desireMove;

    // Update is called once per frame

    void Update()
    {
        var isShiftPressed = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;
        var verticalTotalSpeed = verticalMovementSpeed + isShiftPressed * verticalSprintSpeed;
        var horizontalTotalSpeed = horizontalMovementSpeed + isShiftPressed * horizontalSprintSpeed;

        var forwardMove = transform.rotation * Vector3.forward * verticalTotalSpeed * Input.GetAxis("Vertical");
        var rightMove = transform.rotation * Vector3.right * horizontalTotalSpeed * Input.GetAxis("Horizontal");

        desireMove = rightMove + forwardMove;
        transform.position += desireMove;
    }
}

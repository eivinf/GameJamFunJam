using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxVerticalMovementSpeed;
    public float verticalMovementSpeed;
    public float horizontalMovementSpeed;
    public float maxHorizontalMovementSpeed;
    public Vector3 desireMove;
    public Transform anchor;
    public float ropeLength;
    public float ropeForce;
    public float ropeStretchForce;
    public float gravity;
    public bool isSwinging;
    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity += Vector3.down * gravity;

        //GetComponent<Rigidbody>().velocity -= desireMove;
        if (Mathf.Abs(Vector3.Dot(transform.rotation * Vector3.forward, GetComponent<Rigidbody>().velocity)) < maxVerticalMovementSpeed)
        {
            GetComponent<Rigidbody>().velocity += transform.rotation * Vector3.forward * verticalMovementSpeed * Input.GetAxis("Vertical");
        }
        if (Mathf.Abs(Vector3.Dot(transform.rotation * Vector3.right, GetComponent<Rigidbody>().velocity)) < maxHorizontalMovementSpeed)
        {
            GetComponent<Rigidbody>().velocity += transform.rotation * Vector3.right * horizontalMovementSpeed * Input.GetAxis("Horizontal");
        }

        var moveTowardsAnchor = Mathf.Max(Vector3.Dot(GetComponent<Rigidbody>().velocity, (anchor.position - transform.position)), 0f);
        if (Vector3.Distance(anchor.position, transform.position) > ropeLength && isSwinging)
        {
            GetComponent<Rigidbody>().velocity -= (anchor.position - transform.position).normalized* moveTowardsAnchor * ropeForce;
            GetComponent<Rigidbody>().velocity += (anchor.position - transform.position).normalized * ((anchor.position - transform.position).magnitude - ropeLength) * ropeStretchForce;

        }
        if (Input.GetMouseButtonDown(0) && isSwinging)
        {
            isSwinging = false;
        }
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            var hit = new RaycastHit();
            if (Physics.Raycast(camera.position, camera.forward, out hit, 1000))
            {
                ropeLength = Vector3.Distance(transform.position, hit.point);
                anchor.position = hit.point;
                isSwinging = true;
            }
        }
        

        if (Input.GetMouseButton(1))
        {
            ropeLength -= 0.01f;
        }
        //transform.position += desireMove;
        //desireMove -= (anchor.position - transform.position).normalized * Vector3.Dot(desireMove, (anchor.position - transform.position));
    }
}

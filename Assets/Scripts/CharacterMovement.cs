using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CharacterMovement : MonoBehaviour
{
    public float verticalMovementSpeed;
    public float horizontalMovementSpeed;
    public Text hookedText;

    public float verticalAirMovementSpeed;
    public float horizontalAirMovementSpeed;
    public int jumpsLeft = 1;
    public Vector3 velocity;
    public Transform anchor;
    public float minRopeLength;
    public float ropeLength;
    public float ropeStretchForce;
    public float gravity;
    public float reelGravity;
    public bool isSwinging;
    public Transform camera;
    public bool grounded;
    public int CollisionItterations;
    public float airDrag;
    public float groundDrag;
    public float ReelInSpeed;
    public float jumpForce;
    public float maxMoveSpeed;
    public LayerMask collisionMask;
    public RopeVisulize rope;
    public Transform ropeTarget;
    public AudioSource whip;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetMouseButton(1) && ropeLength > minRopeLength)    // For reeling in vine with "right mouse click"
        //if (Input.GetKey(KeyCode.Q) && ropeLength > minRopeLength)     // For reeling in vine with "Q"
        {
            ropeLength -= ReelInSpeed * Time.deltaTime;
            rope.ReelIn(ReelInSpeed * Time.deltaTime);
            ropeLength = Mathf.Max(ropeLength, minRopeLength);
            velocity += Vector3.down * reelGravity;
        }
        else
        {
            velocity += Vector3.down * gravity;
        }

        if (grounded)
        {
            jumpsLeft = 1;
            velocity += transform.rotation * Vector3.forward * verticalMovementSpeed * Input.GetAxis("Vertical");
            velocity += transform.rotation * Vector3.right * horizontalMovementSpeed * Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity += Vector3.up * jumpForce;
            }
        }
        else if(jumpsLeft > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            velocity += Vector3.up * jumpForce;
            jumpsLeft -= 1;
        }
        else
        {
            velocity += transform.rotation * Vector3.forward * verticalAirMovementSpeed * Input.GetAxis("Vertical");
            velocity += transform.rotation * Vector3.right * horizontalAirMovementSpeed * Input.GetAxis("Horizontal");
        }
        if (isSwinging)
        {
            var moveTowardsAnchor = Mathf.Min(Vector3.Dot(velocity, (anchor.position - transform.position).normalized), 0f);
            if (Vector3.Distance(anchor.position, transform.position) > ropeLength && isSwinging)
            {
                velocity -= (anchor.position - transform.position).normalized * moveTowardsAnchor;
                var ropeStrech = Mathf.Max((anchor.position - transform.position).magnitude - ropeLength, 0f);
                velocity += (anchor.position - transform.position).normalized * ropeStrech * ropeStretchForce;

            }
        }
        
       
        //transform.position += desireMove;
        //desireMove -= (anchor.position - transform.position).normalized * Vector3.Dot(desireMove, (anchor.position - transform.position));

        if (Physics.Raycast(transform.position, Vector3.down, 1.3f, collisionMask))
        {
            if (!grounded) 
            {
                velocity *= 0.01f;
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        for (int i = 0; i < CollisionItterations; i++)
        {
            CheckCollision();
        }

        if (grounded)
        {
            velocity *= (1f - groundDrag);
        }
        else
        {
            velocity *= (1f - airDrag);
        }

        CheckMovedCollision();

        if (velocity.magnitude > maxMoveSpeed)
        {
            velocity *= maxMoveSpeed / velocity.magnitude;
        }

        transform.position += velocity * 50 * Time.deltaTime;

        //if (Input.GetMouseButtonUp(0) && isSwinging)     // For releasing vine by releasing "left mouse click"
        if (Input.GetKeyUp(KeyCode.Q) && isSwinging)   // For releasing vine with "Q"
        {
            rope.Detatch();
            isSwinging = false;
            hookedText.text = "Unhooked";
        }
        if (Input.GetMouseButtonDown(0))
        {
            var hit = new RaycastHit();
            if (Physics.Raycast(camera.position, camera.forward, out hit, 1000, collisionMask))
            {
                whip.Play();
                ropeLength = Mathf.Max(Vector3.Distance(transform.position, hit.point), minRopeLength);
                anchor.position = hit.point;
                rope.Attatch(anchor.position, ropeTarget);
                isSwinging = true;
                hookedText.text = "Hooked";
            }
        }


    }

    public void CheckCollision()
    {
        var radius = 0.5f;
        var point1 = transform.position + Vector3.up * 0.5f;
        var point2 = transform.position + Vector3.down * 0.5f;
        var closestColliderToLine = GetClosestColliderToLine(point1, point2, radius);
        if (closestColliderToLine.Item2 < radius - 0.0001f)
        {
            transform.position -= closestColliderToLine.Item3 * (0.5f - closestColliderToLine.Item2);
            //velocity = FlattenDirectionForVector(velocity, -closestColliderToLine.Item3);
        }
        
    }

    public void CheckMovedCollision()
    {
        var radius = 0.5f;
        var point1 = transform.position + Vector3.up * 0.5f + velocity;
        var point2 = transform.position + Vector3.down * 0.5f + velocity;
        var closestColliderToLine = GetClosestColliderToLine(point1, point2, radius);
        if (closestColliderToLine.Item2 < radius- 0.0001f)
        {
            velocity = FlattenDirectionForVector(velocity, closestColliderToLine.Item1 - transform.position) * 0.5f;
        }

    }

    public Vector3 FlattenDirectionForVector(Vector3 vector, Vector3 direction)
    {
        var projection = Vector3.Dot(vector, direction.normalized) * direction.normalized;
        return vector - projection;
    }

    public (Vector3, float, Vector3) GetClosestColliderToLine(Vector3 point1, Vector3 point2, float radius)
    {
        var ClosestPointDistance1 = CastRayCastCylinder(point1, point2, 5, 8, radius);
        var ClosestPointDistance2 = CastRayHalfShphere(-1f, point1, 5, 8, radius);
        var ClosestPointDistance3 = CastRayHalfShphere(1f , point2, 5, 8, radius);
        if (ClosestPointDistance1.Item2 <= ClosestPointDistance2.Item2 && ClosestPointDistance1.Item2 <= ClosestPointDistance3.Item2)
        {
            return ClosestPointDistance1;
        }

        if (ClosestPointDistance2.Item2 <= ClosestPointDistance1.Item2 && ClosestPointDistance2.Item2 <= ClosestPointDistance3.Item2)
        {
            return ClosestPointDistance2;
        }
        return ClosestPointDistance3;

    }

    public (Vector3, float, Vector3) CastRayCastCylinder(Vector3 point1, Vector3 point2, float layers, float pointsInCircle, float radius)
    {
        var ClosestPointDistance = (Vector3.zero, Mathf.Infinity, Vector3.zero);

        for (float i = 0; i < layers; i++)
        {
            var origin = point1 + (point2 - point1) * (i / (layers -1));
            for (float e = 0; e < pointsInCircle; e++)
            {
                var direction = Quaternion.Euler(0, 360f * (e / pointsInCircle), 0) * Vector3.forward;
                var hit = new RaycastHit();
                //Debug.DrawLine(origin, origin + direction * radius, Color.red, 10f);
                if (Physics.Raycast(origin, direction, out hit, radius, collisionMask))
                {
                    if (hit.distance < ClosestPointDistance.Item2)
                    {
                        ClosestPointDistance = (hit.point, hit.distance, direction);
                    }
                }
            }
        }
        return ClosestPointDistance;
    }

    public (Vector3, float, Vector3) CastRayHalfShphere(float flip, Vector3 origin, float layers, float pointsInCircle, float radius)
    {
        var ClosestPointDistance = (Vector3.zero, Mathf.Infinity, Vector3.zero);


        var hit = new RaycastHit();
        //Debug.DrawLine(origin, origin + Vector3.up* -flip * radius, Color.red, 10f);
        if (Physics.Raycast(origin, Vector3.up * -flip, out hit, radius, collisionMask))
        {
            if (hit.distance < ClosestPointDistance.Item2)
            {
                ClosestPointDistance = (hit.point, hit.distance, Vector3.up * -flip);
            }
        }

        for (float i = 1; i < layers; i++)
        {
            for (float e = 0; e < pointsInCircle; e++)
            {
                var direction = Quaternion.Euler(90f * (i / layers) * flip, 0, 0) * Vector3.forward;
                direction = Quaternion.Euler(0, 360f * (e / pointsInCircle), 0) * direction;
                
                //Debug.DrawLine(origin, origin + direction * radius, Color.red, 10f);
                if (Physics.Raycast(origin, direction, out hit, radius, collisionMask))
                {
                    if (hit.distance < ClosestPointDistance.Item2)
                    {
                        ClosestPointDistance = (hit.point, hit.distance, direction);
                    }
                }
            }
        }
        return ClosestPointDistance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float horizontalMovementSpeed;
    public float verticalMovementSpeed;
    public float maxPitch;
    public float minPitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pitch = -Input.GetAxis("Mouse Y") * verticalMovementSpeed;
        transform.rotation *= Quaternion.Euler(pitch, 0, 0);

        var yaw = Input.GetAxis("Mouse X") * horizontalMovementSpeed;
        transform.parent.rotation *= Quaternion.Euler(0, yaw, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;

    public float rotSpeed = 10f;
    public float movSpeed = 10f;

    float rotation_y = 0.0f;

    void FixedUpdate()
    {
        //follow target
        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * movSpeed);
    }

    void Update()
    {
        //rotate camera
        float mouse_x = Input.mousePosition.x;
        rotation_y += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotation_y, 0);
    }
}

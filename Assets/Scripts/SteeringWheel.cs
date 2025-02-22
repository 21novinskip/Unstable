using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{

    public Transform orientation;
    public Collider2D collider;
    private bool wheelGrabbed = false;
    private float wheel_z = 0f;
    private Vector3 previousMousePosition = new Vector3(0, 0, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public bool IsTouchingMouse()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider.OverlapPoint(point);
    }
    // Update is called once per frame
    void Update()
    {
        //print("MOUSE POS: "+Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && IsTouchingMouse())
        {
            wheelGrabbed = true;
            print("GRISP");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            wheelGrabbed = false;
        }
    }
    void FixedUpdate()
    {
        if (wheelGrabbed)
        {

            float target_z = wheel_z;
            if (Input.mousePosition.x > previousMousePosition.x)
            {

                target_z = target_z + 100;
            }
            else if (Input.mousePosition.x < previousMousePosition.x)
            {
                target_z = target_z - 100;
               
            }
            else
            {
                target_z = orientation.rotation.z;
            }

            Quaternion target_rotation = Quaternion.Euler(0, 0, target_z);
            orientation.rotation = Quaternion.RotateTowards(orientation.rotation, target_rotation, 1);
            wheel_z = orientation.rotation.z;
            print(wheel_z);
        }


        previousMousePosition = Input.mousePosition;
    }
}

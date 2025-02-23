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
    private Vector3 mousePos;
    private float initialWheelAngle = 0f;  // Initial angle when grabbed
    private Vector3 initialMousePosition;  // Mouse position when grabbed
    public float wheelAngle = 0f;
    private float oldAngle = 0f;
    private float oldDeltaX = 0f;
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
        mousePos = Input.mousePosition;
        //print("MOUSE POS: "+Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && IsTouchingMouse())
        {
            wheelGrabbed = true;
            initialMousePosition = mousePos;
            initialWheelAngle = orientation.eulerAngles.z; // Store initial rotation
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
            RotateWheelWithMouse();
        }

    }
    void RotateWheelWithMouse()
    {
        float sensitivity = 0.15f; // Adjust to make rotation feel right

        float deltaX = mousePos.x - initialMousePosition.x; // Mouse X movement

        float newAngle = initialWheelAngle - deltaX * sensitivity; // Convert movement to rotation
        if (newAngle != oldAngle)
        {
            print("deltaX: "+deltaX);
            wheelAngle -= (deltaX * sensitivity);
        }
        if (Mathf.Abs(deltaX) < Mathf.Abs(oldDeltaX))
        {

            initialMousePosition = mousePos;
            initialWheelAngle = orientation.eulerAngles.z; // Store initial rotation
        }
        oldDeltaX = deltaX;
        orientation.rotation = Quaternion.Euler(0, 0, newAngle); // Apply rotation
        oldAngle = newAngle;
    }
}

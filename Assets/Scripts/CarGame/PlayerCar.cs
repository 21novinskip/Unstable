using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{


    public Transform steeringWheelOrientation;
    public Rigidbody2D rb;
    public GameObject steeringWheelObj;
    public SteeringWheel steeringWheelScript;
    public float speed;
    private float steeringWheelZAngle;
    private float steeringWheelZAnglePrev = 0f;
    public float upperBounds;
    public float lowerBounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        steeringWheelScript = steeringWheelObj.GetComponent<SteeringWheel>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        steeringWheelZAngle = steeringWheelScript.wheelAngle;
        
        if (steeringWheelZAngle > steeringWheelZAnglePrev && transform.position.y < upperBounds)
        {
            rb.linearVelocity = new Vector2(0f, speed);
        }
        else if (steeringWheelZAngle < steeringWheelZAnglePrev && transform.position.y > lowerBounds)
        {
            rb.linearVelocity = new Vector2(0f, -speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, 0f);
        }
        steeringWheelZAnglePrev = steeringWheelScript.wheelAngle;
    }

}

using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class FlyHandler : MonoBehaviour
{
    public float speed;
    public float moveTime;
    private float moveTimer = -0.2f;
    public Rigidbody2D rb;
    public Transform transform;
    public float lifeTime;
    private float lifeTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.9f);
        Vector2 direction = (new Vector2(0f, 0f) - (Vector2)transform.position).normalized;
        rb.AddForce(direction * 10f, ForceMode2D.Impulse); // Apply force towards center
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        moveTimer += 0.01f;
        lifeTimer += 0.01f;
        if (moveTimer > moveTime)
        {
            ChangeDirection();
            moveTimer = 0f;
        }

        float xMod = UnityEngine.Random.Range(-1f, 1f);
        float yMod = UnityEngine.Random.Range(-1f, 1f);
        Vector2 tempSpeed = rb.linearVelocity;
        tempSpeed.x += xMod;
        tempSpeed.y += yMod;
        rb.linearVelocity = tempSpeed;
    }
    void OnBecameInvisible()
    {
        if (lifeTimer > lifeTime)
        {
            Destroy(gameObject);
        }
    }
    void ChangeDirection()
    {
        float xSpeed = UnityEngine.Random.Range(-speed, speed);
        float ySpeed = UnityEngine.Random.Range(-speed, speed);
        rb.linearVelocity = new Vector2(xSpeed, ySpeed);
    }
}

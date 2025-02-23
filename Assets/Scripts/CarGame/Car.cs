using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("COLLISION");
        if (other.gameObject.tag == "car")
        {
            Destroy(gameObject);
        }
    }
}

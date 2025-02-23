using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public SpriteRenderer renderer;
    public Sprite carSprite1;
    public Sprite carSprite2;
    public Sprite carSprite3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(speed, 0f);
        int spriteNum = UnityEngine.Random.Range(1, 3);
        switch (spriteNum)
        {
            case 1:
                renderer.sprite = carSprite1;
                break;
            case 2:
                renderer.sprite = carSprite2;
                break;
            case 3:
                renderer.sprite = carSprite3;
                break;

        }
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

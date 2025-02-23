using UnityEngine;

public class CarGameHandler : MonoBehaviour
{

    public float minigameTime;
    private float minigameTimer = 0f;
    public string[] modifiers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        minigameTimer += 0.01f;
        if (minigameTimer > minigameTime)
        {
            //do ending minigame stuffs here
        }
    }
}

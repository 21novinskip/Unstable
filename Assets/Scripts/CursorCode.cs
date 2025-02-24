using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Sprite normalCursor; // Drag your normal cursor sprite here
    public bool ChangesOnClick = false;
    public Sprite clickCursor;  // Drag your "mouse down" sprite here
    private Image cursorImage;

    void Start()
    {
        Cursor.visible = false; // Hide default cursor

        // Ensure this object has an Image component (if using UI)
        cursorImage = GetComponent<Image>();

        if (cursorImage == null)
        {
            Debug.LogError("No Image component found! Make sure this script is on a UI Image.");
        }
    }

    void Update()
    {
        // Convert mouse position from screen to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure it's on the correct Z-axis (adjust if needed)
        transform.position = mousePosition;

        if (ChangesOnClick)
        {
            // Change sprite based on mouse button state
            if (Input.GetMouseButton(0)) // Left-click
            {
                cursorImage.sprite = clickCursor;
            }
            else
            {
                cursorImage.sprite = normalCursor;
            }
        }
    }
}

using UnityEngine;
using TMPro;

public class EmailScript : MonoBehaviour
{
    private Vector3 startPos;
    public float returnSpeed = 5f; // Speed at which it returns to center
    public float swipeAwayThreshold = 3f; // Distance required to swipe away
    public float swipeSpeed = 10f; // Speed at which it moves away
    private bool isSwipingAway = false;
    private Vector3 swipeTarget; // Target position for swiping away
    private EmailManager emailManager; // Reference to the email manager

    public TextMeshProUGUI titleText; // Reference to the TextMeshPro component

    public string SpamOrNah; // Variable to hold either "Spam" or "Important"

    void Start()
    {
        startPos = transform.position;

        // Get a reference to the EmailManager
        emailManager = FindObjectOfType<EmailManager>();

        // Find the child TextMeshPro object and store reference
        titleText = GetComponentInChildren<TextMeshProUGUI>();

        // Randomly assign "Spam" or "Important" to SpamOrNah
        SpamOrNah = (Random.Range(0, 2) == 0) ? "Spam" : "Important";
        Debug.Log("The email is: " + SpamOrNah); // Print to console

        // Assign a title from the appropriate list
        if (SpamOrNah == "Spam" && emailManager.spamTitles.Count > 0)
        {
            int randomIndex = Random.Range(0, emailManager.spamTitles.Count);
            titleText.text = emailManager.spamTitles[randomIndex];
        }
        else if (SpamOrNah == "Important" && emailManager.importantTitles.Count > 0)
        {
            int randomIndex = Random.Range(0, emailManager.importantTitles.Count);
            titleText.text = emailManager.importantTitles[randomIndex];
        }
    }

    void Update()
    {
        if (isSwipingAway)
        {
            // Move towards the swipe-away target position
            transform.position = Vector3.Lerp(transform.position, swipeTarget, swipeSpeed * Time.deltaTime);

            // Check if the email has moved off-screen
            if (Mathf.Abs(transform.position.x - swipeTarget.x) < 0.1f)
            {
                emailManager.HandleSwipe(this.gameObject); // Call HandleSwipe in the manager to handle removal
            }
            return;
        }

        if (Input.GetMouseButton(0)) // Holding left mouse button
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z); // Use correct depth
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(mousePos.x, startPos.y, startPos.z); // Drag left/right
        }
        else
        {
            float distanceDragged = transform.position.x - startPos.x; // How far did we move?

            if (Mathf.Abs(distanceDragged) > swipeAwayThreshold) // Swiped far enough?
            {
                // Decide swipe direction
                float direction = Mathf.Sign(distanceDragged); // -1 (left) or 1 (right)
                swipeTarget = startPos + new Vector3(direction * 200f, 0, 0); // Move it far offscreen
                isSwipingAway = true; // Start swipe-away effect
            }
            else
            {
                // Return to center if not swiped far enough
                transform.position = Vector3.Lerp(transform.position, startPos, returnSpeed * Time.deltaTime);
            }
        }
    }
}






using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public float swipeSpeed = 10f; // Speed at which emails slide up
    public float swipeAwayThreshold = 3f; // Distance to swipe away
    private List<GameObject> emailList = new List<GameObject>(); // List of emails in the scene
    private bool isSwipingAway = false; // Whether an email is being swiped away
    private GameObject emailToSwipeAway = null; // Email that's being swiped away

    public List<string> spamTitles = new List<string>(); // List for spam email titles
    public List<string> importantTitles = new List<string>(); // List for important email titles

    // The transform for the top email (where others will slide up from)
    public Transform topEmailPosition;

    void Start()
    {
        // Find all email objects in the scene and add them to the emailList
        emailList.AddRange(GameObject.FindGameObjectsWithTag("Email")); // Ensure your email objects are tagged as "Email"

        // Assign titles randomly to each email
        foreach (var email in emailList)
        {
            EmailScript emailScript = email.GetComponent<EmailScript>();
            if (emailScript != null)
            {
                emailScript.SpamOrNah = (Random.Range(0, 2) == 0) ? "Spam" : "Important";
                AssignRandomTitle(emailScript);
            }
        }
    }

    void Update()
    {
        if (emailToSwipeAway != null)
        {
            // Move the swiped email away off-screen
            emailToSwipeAway.transform.position = Vector3.Lerp(emailToSwipeAway.transform.position, 
                new Vector3(0, -1000f, 0), swipeSpeed * Time.deltaTime); // Move it down/offscreen

            if (Vector3.Distance(emailToSwipeAway.transform.position, new Vector3(0, -1000f, 0)) < 1f)
            {
                // Once the email is out of view, trigger the shift of other emails
                ShiftEmailsUp();
                emailToSwipeAway.SetActive(false); // Hide the swiped email (or destroy if preferred)
                emailToSwipeAway = null;
            }
        }
        else
        {
            // Handle swipe input for each email
            foreach (var email in emailList)
            {
                if (email.activeSelf && Input.GetMouseButton(0)) // Check if mouse is held
                {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                    // Check if the email is being swiped (rough detection based on mouse position)
                    if (Vector3.Distance(email.transform.position, mousePos) < 50f) // Assuming you are dragging the email
                    {
                        email.transform.position = new Vector3(mousePos.x, email.transform.position.y, email.transform.position.z);
                    }
                }
                else if (email.activeSelf)
                {
                    // If the mouse isn't being held, check if we need to swipe the email away
                    float distanceDragged = email.transform.position.x - (emailList[0].transform.position.x);

                    if (Mathf.Abs(distanceDragged) > swipeAwayThreshold)
                    {
                        emailToSwipeAway = email; // Set the email to be swiped away
                        break; // Stop checking others once one is selected for swiping
                    }
                }
            }
        }
    }

    void ShiftEmailsUp()
    {
        // Slide all emails up after one is swiped away
        for (int i = 0; i < emailList.Count; i++)
        {
            if (emailList[i].activeSelf)
            {
                Vector3 targetPosition = new Vector3(emailList[i].transform.position.x, 
                    topEmailPosition.position.y + (i * 200f), 0); // Adjust vertical positioning based on the top position
                emailList[i].transform.position = targetPosition;
            }
        }
    }

    void AssignRandomTitle(EmailScript emailScript)
    {
        // Randomly choose a title from the appropriate list
        if (emailScript.SpamOrNah == "Spam" && spamTitles.Count > 0)
        {
            int randomIndex = Random.Range(0, spamTitles.Count);
            emailScript.titleText.text = spamTitles[randomIndex];
        }
        else if (emailScript.SpamOrNah == "Important" && importantTitles.Count > 0)
        {
            int randomIndex = Random.Range(0, importantTitles.Count);
            emailScript.titleText.text = importantTitles[randomIndex];
        }
    }

    // New method for handling the swipe
    public void HandleSwipe(GameObject email)
    {
        // Remove the email from the list
        emailList.Remove(email);
        Destroy(email); // Optionally destroy the email if it's removed
    }
}





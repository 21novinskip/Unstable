using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public float swipeSpeed = 10f; // Speed at which emails slide up
    public float swipeAwayThreshold = 3f; // Distance to swipe away
    private List<GameObject> emailList = new List<GameObject>(); // List of emails in the scene

    public List<string> spamTitles = new List<string>(); // List for spam email titles
    public List<string> importantTitles = new List<string>(); // List for important email titles

    // The transform for the top email (where others will slide up from)
    public Transform topEmailPosition;

    public float heightdif = 138;

    public int CurrentNumberOfEmails = 5;

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

    }

    void ShiftEmailsUp()
    {
        emailList.Clear(); // Cleanup destroyed objects
        emailList.AddRange(GameObject.FindGameObjectsWithTag("Email"));

        // Lerp the emails upwards by 138 units
        for (int i = 0; i < emailList.Count; i++)
        {
            if (emailList[i] != null)
            {
                Vector3 targetPosition = new Vector3(
                    emailList[i].transform.position.x,
                    emailList[i].transform.position.y + heightdif, // Move email up
                    emailList[i].transform.position.z
                );

                // Lerp to new position
                emailList[i].transform.position = Vector3.Lerp(emailList[i].transform.position, targetPosition, swipeSpeed * Time.deltaTime);
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
        emailList.Remove(email);
        //ShiftEmailsUp();  // Shift first before destroying
        Destroy(email, 0.1f); // Delay destruction slightly to prevent reference issues
        CurrentNumberOfEmails --;
        Debug.Log("Currently " + CurrentNumberOfEmails);

        if (CurrentNumberOfEmails == 0)
        {
            Debug.Log("Empty");
        }
    }
}





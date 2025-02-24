using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

public class EmailManager : MonoBehaviour
{
    public float swipeSpeed = 10f; // Speed at which emails slide up
    public float swipeAwayThreshold = 3f; // Distance to swipe away
    private List<GameObject> emailList = new List<GameObject>(); // List of emails in the scene

    public List<string> spamTitles = new List<string>(); // List for spam email titles
    public List<string> importantTitles = new List<string>(); // List for important email titles

    // The transform for the top email (where others will slide up from)
    public Transform topEmailPosition;
    public GameObject sceneControllerObj;
    private SceneController sceneControllerScript;
    public float heightdif = 138;
    private int currentDay = 1;
    public int CurrentNumberOfEmails = 5;
    void Start()
    {
        sceneControllerObj = GameObject.Find("Scene Controller");
        sceneControllerScript = sceneControllerObj.GetComponent<SceneController>();
        switch (currentDay)
        {
            case 1:
                importantTitles.Add("Safe Healthcare - Update on your Health Insurance");
                importantTitles.Add("City Energy Department - Your November Utilities Bill");
                importantTitles.Add("Medicare - Application for Medical Assistance");
                spamTitles.Add("h8rgirl - Why Your Life Sucks (37/129)");
                spamTitles.Add("Prottecc-Co - Apply for new fire insurance through Protec-Co!");
                break;
            case 2:
                importantTitles.Add("Luigi Bros Plumbers - Bill for Burst Pipe.");
                importantTitles.Add("Shaun Landlord - Memo: Your Rent is Going Up.");
                importantTitles.Add("Westeastern University - Student Loan Payments for the Year.");
                spamTitles.Add("hotjess66969 - Hot Singles in your Area!");
                spamTitles.Add("Unknown Sender - So, about your oven.");
                break;
            case 3:
                importantTitles.Add("City Energy Department - Gas Leak Detected at your Apartment.");
                importantTitles.Add("St. Jesus Hospital - New Health Insurance Declined.");
                importantTitles.Add("Emily Ferros - Cody, join us for coffee tomorrow!");
                spamTitles.Add("angelarg@weirdd.co - Hey Pea-Brain. You Teleport?");
                spamTitles.Add("Unknown Sender - Listen, we need to talk about your oven.");
                break;
            case 4:
                importantTitles.Add("City Energy Department - Oven on Fire + Gas Leak = Explosion Imminent. Please Evacuate");
                importantTitles.Add("Medicare - Medical Assistance Application: Denied");
                importantTitles.Add("St. Jesus Hospital - Your New Medical Bill.");
                importantTitles.Add("hotjess66969 - Hot Singles in your Area!");
                spamTitles.Add("Unknown Sender - Your oven is on fire!");
                break;
        }
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        emailList = emailList.OrderByDescending(x => x.transform.position.y).ToList();
=======

        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        StartCoroutine(command.WaitThirty());
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        StartCoroutine(command.WaitThirty());

        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        StartCoroutine(command.WaitThirty());

        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        StartCoroutine(command.WaitThirty());
    }

    void Update()
    {
        if (emailList.Count == 0)
        {
            sceneControllerScript.LoadScene(sceneControllerScript.NextScene);
        }
    }

    void ShiftEmailsUp(GameObject email)
    {
        emailList.Clear(); // Cleanup destroyed objects
        emailList.AddRange(GameObject.FindGameObjectsWithTag("Email"));

        // Lerp the emails upwards by 138 units
        for (int i = emailList.IndexOf(email); i < emailList.Count; i++)
        {
            if (emailList[i] != null && emailList[i].transform.position.y <= 307f && emailList[Mathf.Clamp(i, 0, emailList.Count)].transform.position.y <= 370f)
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
            spamTitles.RemoveAt(randomIndex);
        }
        else if (emailScript.SpamOrNah == "Important" && importantTitles.Count > 0)
        {
            int randomIndex = Random.Range(0, importantTitles.Count);
            emailScript.titleText.text = importantTitles[randomIndex];
            importantTitles.RemoveAt(randomIndex);
        }
    }

    // New method for handling the swipe
    public void HandleSwipe(GameObject email)
    {
        emailList.Remove(email);
        //ShiftEmailsUp(email);  // Shift first before destroying
        Destroy(email, 0.1f); // Delay destruction slightly to prevent reference issues
        CurrentNumberOfEmails --;
        Debug.Log("Currently " + CurrentNumberOfEmails);
        bool emailsAllOffScreen = true;
        /*for (int i = 0; i < emailList.Count; i++)
        {
            if (emailList[i].transform.position.y >= 260)
            {
                emailsAllOffScreen = false;
            }
        }
        if (emailsAllOffScreen)
        {
            for (int i = 0; i < emailList.Count; i++)
            {
                float yPos = emailList[i].transform.position.y;
                emailList[i].transform.position = new Vector3(emailList[i].transform.position.x, yPos + 362, emailList[i].transform.position.z);
            }
        }*/
        if (CurrentNumberOfEmails == 0)
        {
            Debug.Log("Empty");
        }
    }
}





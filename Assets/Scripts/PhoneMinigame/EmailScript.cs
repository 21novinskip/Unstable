using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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


        private PointerEventData pointerEventData;
        private List<RaycastResult> raycastResults = new List<RaycastResult>();

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
            transform.position = Vector3.Lerp(transform.position, swipeTarget, swipeSpeed * Time.deltaTime);
            
            if (Mathf.Abs(transform.position.x - swipeTarget.x) < 0.1f) //if I've made it to theswipetarget
            {
                emailManager.HandleSwipe(this.gameObject); //kill it yurt
                isSwipingAway = false;
            }
            return;
        }

        // if being dragged
        if (Input.GetMouseButton(0) && IsMouseOverUI()) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            transform.position = new Vector3(mousePos.x, transform.position.y, startPos.z);
        }
        else //if not being dragged
        {
            float distanceDragged = transform.position.x - startPos.x;
            if (Mathf.Abs(distanceDragged) > swipeAwayThreshold) //if the distance dragged is above the threshhold
            {
                if (!isSwipingAway) //if not swiping yet, start lol
                {
                    Vector3 swipePos = startPos;
                    swipePos.y = transform.position.y;
                    float direction = Mathf.Sign(distanceDragged); //figure out if going left or right
                    swipeTarget = startPos + new Vector3(direction * swipeAwayThreshold*2f, 0, 0); //make a target
                    swipeTarget.y = transform.position.y;
                    isSwipingAway = true;
                }
            }
            else 
            {
                //if not being dragged and not beyond threshold,-
                if (transform.position.x != startPos.x) //            - and not at home base, return to base
                {
                    // Only modify the X position, keep Y and Z the same
                    transform.position = new Vector3(
                        Mathf.Lerp(transform.position.x, startPos.x, returnSpeed * Time.deltaTime),  // Lerp the X position
                        transform.position.y,  // Keep the Y position the same
                        transform.position.z   // Keep the Z position the same
                    );
                }
            }
        }
    }

    private bool IsMouseOverUI()
    {
        pointerEventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == gameObject) // Ensure the UI element under the cursor is THIS one
            {
                return true;
            }
        }
        return false;
    }


}






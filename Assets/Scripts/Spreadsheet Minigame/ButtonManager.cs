using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public float minigameTime;
    private float minigameTimer = 0f;
    public TMP_Text timerText;

    private int Tasks_Completed;
    public int CorrectButtons = 0;
    public TextMeshProUGUI ScoreText;
    public int Digits = 2;

    public SpriteRenderer carInstructions;
    private float carInstructionsAlpha = 0f;

    public Button[] buttons; // Array to hold all 20 buttons

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.visible = false;

        Tasks_Completed = 0;
        ScoreText.text = "Duplicates found: " + Tasks_Completed.ToString();
        ResetButtons();

        GameObject obj = GameObject.Find("Scene Controller");
        SceneController command = obj.GetComponent<SceneController>();
        StartCoroutine(command.WaitThirty());

        Color startColor = carInstructions.color;
        carInstructions.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        carInstructionsAlpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetButtons();
        }

        if (CorrectButtons == 2)
        {
            Win();
        }
    }

    void FixedUpdate()
    {
        minigameTimer += Time.fixedDeltaTime;
        //Debug.Log(minigameTimer);
        

        if (minigameTimer < 2f)
        {
            carInstructionsAlpha += 0.1f;
        }
        else if ((minigameTimer >=2f) && (minigameTimer < 4f))
        {
            carInstructionsAlpha -= 0.1f;
        }
        else if (minigameTimer >= 4)
        {
            timerText.text = ((int)(35 - minigameTimer)).ToString();
        }
        Color startColor = carInstructions.color;
        carInstructions.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Clamp(carInstructionsAlpha, 0f, 1f));
    }

    void ResetButtons()
    {

        foreach (var button in buttons)
        {
            button.GetComponent<ButtonScript>().IsDupe = false;
        }

        CorrectButtons = 0;

        // Create a list to keep track of unique numbers
        List<int> generatedNumbers = new List<int>();

        // Generate unique numbers for each button
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = GenerateRandomNumber(Digits);
            } 
            while (generatedNumbers.Contains(randomNumber)); // Ensure uniqueness

            generatedNumbers.Add(randomNumber);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = randomNumber.ToString();
        }

        // Select two random buttons to be the "dupe" ones
        int rando1 = Random.Range(0, buttons.Length);
        int rando2;
        do
        {
            rando2 = Random.Range(0, buttons.Length);
        } while (rando2 == rando1);

        

        // Generate a unique dupe number that is NOT in generatedNumbers
        int dupeNumber;
        do
        {
            dupeNumber = GenerateRandomNumber(Digits);
        } while (generatedNumbers.Contains(dupeNumber)); // Ensure it's unique

        // Assign the same number to both dupe buttons
        buttons[rando1].GetComponentInChildren<TextMeshProUGUI>().text = dupeNumber.ToString();
        buttons[rando2].GetComponentInChildren<TextMeshProUGUI>().text = dupeNumber.ToString();
        Debug.Log("Buttons " + (rando1 + 1) + " and " + (rando2 + 1) + " are both " + dupeNumber);


        // Mark these buttons as "dupe"
        buttons[rando1].GetComponent<ButtonScript>().IsDupe = true;
        buttons[rando2].GetComponent<ButtonScript>().IsDupe = true;

        DeselectAllButtons();
    }

    public void Win()
    {
        Debug.Log("You win this round!");
        Tasks_Completed ++;
        ScoreText.text = "Duplicates found: " + Tasks_Completed.ToString();
        ResetButtons();
    }
    public void Lose()
    {
        Debug.Log("You Lost this round");
        ResetButtons();
    }

    // Function to generate a random number with a specified number of digits
    int GenerateRandomNumber(int digits)
    {
        int min = (int)Mathf.Pow(10, digits - 1); // Explicit cast to int
        int max = (int)Mathf.Pow(10, digits) - 1;  // Explicit cast to int

        return Random.Range(min, max + 1); // Generate random number in the specified range
    }

    public void DeselectAllButtons()
    {
        // Ensure no button is selected in EventSystem
        EventSystem.current.SetSelectedGameObject(null);
        Debug.Log("Deselcting buttons");
    }

}

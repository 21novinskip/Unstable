using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;


public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public string MessageInABottle = "Hey Guys";
    public string CurrentPoint = "Sample Scene";
    public int CurrentDay = 1;
    public string NextScene = "Transition(WHAT)";

    private string storedData = "";

    [Header("Sprite Shit")]
    public Sprite day1Sprite;
    public Sprite day2Sprite;
    public Sprite day3Sprite;
    public Sprite day4Sprite;
    public Sprite day5Sprite;
    public Sprite driveInstructionSprite;
    public Sprite spreadsheetInstructionSprite;
    public Sprite emailInstructionSprite;
    public UnityEngine.UI.Image image;
    public float fadeInDuration = 2f;  // Duration to fade in (seconds)
    public float stayDuration = 1.5f;  // Duration to stay fully visible (seconds)
    public float fadeOutDuration = 2f;  // Duration to fade out (seconds)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the SceneManager between scenes

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadSceneWithData(NextScene, MessageInABottle);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentPoint = "Day 1 Intro";
            LoadScene(NextScene);
        }
    }

    public void LoadSceneWithData(string sceneName, string data)
    {
        storedData = data;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string GetStoredData()
    {
        return storedData;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Debug.Log("New Scene Loaded: " + scene.name);
        UnityEngine.Debug.Log(MessageInABottle);

        switch(CurrentPoint)
        {
            case "Day 1 Intro":
                {
                    // Find ImageSpot in the whole scene by name
                    GameObject imageSpotObject = GameObject.Find("ImageSpot");

                    if (imageSpotObject != null)
                    {
                        // Get the Image component from ImageSpot
                        image = imageSpotObject.GetComponent<UnityEngine.UI.Image>();

                        if (image != null && day1Sprite != null)
                        {
                            // Set the sprite to "Day1"
                            image.sprite = day1Sprite;

                            // Start the fade process
                            StartCoroutine(FadeInAndOut("CarMinigame", "Day 1 Car Game"));
                        }
                        else { UnityEngine.Debug.LogError("Image component or Day1 sprite is missing."); }
                    }
                    else { UnityEngine.Debug.LogError("ImageSpot object not found in the scene."); }
                    break;
                }
            case "Day 1 Car":


                StartCoroutine(FadeInAndOut("SpreadsheetMinigame", "Day 1 Spreadsheet Game"));
                break;
            case "Day 1 Office":

                //to emails day 1 when done
                break;
            case "Day 1 Email":
                StartCoroutine(FadeInAndOut("Transition(WHAT)", "Day 2 Intro"));
                break;
            //day 2
            case "Day 2 Intro":
                {
                    // Find ImageSpot in the whole scene by name
                    GameObject imageSpotObject = GameObject.Find("ImageSpot");

                    if (imageSpotObject != null)
                    {
                        // Get the Image component from ImageSpot
                        image = imageSpotObject.GetComponent<UnityEngine.UI.Image>();

                        if (image != null && day1Sprite != null)
                        {
                            // Set the sprite to "Day1"
                            image.sprite = day1Sprite;

                            // Start the fade process
                            StartCoroutine(FadeInAndOut("CarMinigame", "Day 2 Car Game"));
                        }
                        else { UnityEngine.Debug.LogError("Image component or Day2 sprite is missing."); }
                    }
                    else { UnityEngine.Debug.LogError("ImageSpot object not found in the scene."); }
                    break;
                }
            case "Day 2 Car":


                StartCoroutine(FadeInAndOut("SpreadsheetMinigame", "Day 2 Spreadsheet Game"));
                break;
            case "Day 2 Office":

                //to emails day 2 when done
                break;
            case "Day 2 Email":
                StartCoroutine(FadeInAndOut("Transition(WHAT)", "Day 3 Intro"));
                break;
            //day 3
            case "Day 3 Intro":
                {
                    // Find ImageSpot in the whole scene by name
                    GameObject imageSpotObject = GameObject.Find("ImageSpot");

                    if (imageSpotObject != null)
                    {
                        // Get the Image component from ImageSpot
                        image = imageSpotObject.GetComponent<UnityEngine.UI.Image>();

                        if (image != null && day3Sprite != null)
                        {
                            // Set the sprite to "Day3"
                            image.sprite = day3Sprite;

                            // Start the fade process
                            StartCoroutine(FadeInAndOut("CarMinigame", "Day 3 Car Game"));
                        }
                        else { UnityEngine.Debug.LogError("Image component or Day3 sprite is missing."); }
                    }
                    else { UnityEngine.Debug.LogError("ImageSpot object not found in the scene."); }
                    break;
                }
            case "Day 3 Car":


                StartCoroutine(FadeInAndOut("SpreadsheetMinigame", "Day 3 Spreadsheet Game"));
                break;
            case "Day 3 Office":

                //to emails day 3 when done
                break;
            case "Day 3 Email":
                StartCoroutine(FadeInAndOut("Transition(WHAT)", "Day 4 Intro"));
                break;
            //day 4
            case "Day 4 Intro":
                {
                    // Find ImageSpot in the whole scene by name
                    GameObject imageSpotObject = GameObject.Find("ImageSpot");

                    if (imageSpotObject != null)
                    {
                        // Get the Image component from ImageSpot
                        image = imageSpotObject.GetComponent<UnityEngine.UI.Image>();

                        if (image != null && day4Sprite != null)
                        {
                            // Set the sprite to "Day4"
                            image.sprite = day4Sprite;

                            // Start the fade process
                            StartCoroutine(FadeInAndOut("CarMinigame", "Day 4 Car Game"));
                        }
                        else { UnityEngine.Debug.LogError("Image component or Day4 sprite is missing."); }
                    }
                    else { UnityEngine.Debug.LogError("ImageSpot object not found in the scene."); }
                    break;
                }
            case "Day 4 Car":


                StartCoroutine(FadeInAndOut("SpreadsheetMinigame", "Day 4 Spreadsheet Game"));
                break;
            case "Day 4 Office":

                //to emails day 5 when done
                break;
            case "Day 4 Email":
                StartCoroutine(FadeInAndOut("Transition(WHAT)", "Day 5 Intro"));
                break;
            //day 5 (FINAL DAY)
            case "Day 5 Intro":
                {
                    // Find ImageSpot in the whole scene by name
                    GameObject imageSpotObject = GameObject.Find("ImageSpot");

                    if (imageSpotObject != null)
                    {
                        // Get the Image component from ImageSpot
                        image = imageSpotObject.GetComponent<UnityEngine.UI.Image>();

                        if (image != null && day5Sprite != null)
                        {
                            // Set the sprite to "Day1"
                            image.sprite = day5Sprite;

                            // Start the fade process
                            StartCoroutine(FadeInAndOut("CarMinigame", "Day 5 Car Game"));
                        }
                        else { UnityEngine.Debug.LogError("Image component or Day5 sprite is missing."); }
                    }
                    else { UnityEngine.Debug.LogError("ImageSpot object not found in the scene."); }
                    break;
                }
            case "Day 5 Car":


                StartCoroutine(FadeInAndOut("SpreadsheetMinigame", "Day 5 Spreadsheet Game"));
                break;
            case "Day 5 Office":

                //fade into email 5
                break;
            case "Day 5 Email":
                //idk, credits or somethin
                break;
            default:
                break;

        }

        /*if (CurrentPoint == "Day 1 Intro")
        {
            // Find ImageSpot in the whole scene by name
            GameObject imageSpotObject = GameObject.Find("ImageSpot");

            if (imageSpotObject != null)
            {
                // Get the Image component from ImageSpot
                image = imageSpotObject.GetComponent<Image>();

                if (image != null && day1Sprite != null)
                {
                    // Set the sprite to "Day1"
                    image.sprite = day1Sprite;
                    
                    // Start the fade process
                    StartCoroutine(FadeInAndOut("CarMinigame", "Day 1 Car Game"));
                }
                else{Debug.LogError("Image component or Day1 sprite is missing.");}
            }
            else{Debug.LogError("ImageSpot object not found in the scene.");}
        }*/
    }

    private IEnumerator FadeInAndOut(string NextName, string NextPoint)
    {
        // Set the image to completely transparent immediately
        Color startColor = image.color;
        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Wait for the specified fade-in delay
        yield return new WaitForSeconds(1f);

        // Fade in (slowly increase alpha)
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure fully visible at the end of fade-in
        image.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Stay fully visible for the specified time
        yield return new WaitForSeconds(stayDuration);

        // Fade out (slowly decrease alpha)
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure fully transparent at the end of fade-out
        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Print a debug message once the whole process is complete
        UnityEngine.Debug.Log("Image fading complete!");
        
        CurrentPoint = NextPoint;
        LoadScene(NextName);
    }
}


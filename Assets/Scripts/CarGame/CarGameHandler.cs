using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

public class CarGameHandler : MonoBehaviour
{

    public float minigameTime;
    private float minigameTimer = 0f;
    public TMP_Text timerText;
    public string[] modifiers;
    public GameObject sceneControllerObj;
    private SceneController sceneControllerScript;
    public SpriteRenderer carInstructions;
    private float carInstructionsAlpha = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneControllerObj = GameObject.Find("Scene Controller");
        sceneControllerScript = sceneControllerObj.GetComponent<SceneController>();

        Color startColor = carInstructions.color;
        carInstructions.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        carInstructionsAlpha = 0f;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        minigameTimer += Time.fixedDeltaTime;
        if (minigameTimer < 2f)
        {
            carInstructionsAlpha += 0.1f;
        }
        else
        {
            carInstructionsAlpha -= 0.1f;
        }
        Color startColor = carInstructions.color;
        carInstructions.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Clamp(carInstructionsAlpha, 0f, 1f));
        if (minigameTimer > minigameTime)
        {
            //do ending minigame stuffs here
            //go to next scene after time passes
            timerText.text = "YOU WIN!";
            if (minigameTimer > minigameTime + 5)
            {
                print("SCENE TO LOAD: " + sceneControllerScript.NextScene);
                sceneControllerScript.LoadSceneUnderstood();
            }
        }
        else
        {

            timerText.text = ((int)(31 - minigameTimer)).ToString();
        }
    }
}

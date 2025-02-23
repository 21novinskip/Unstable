using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Diagnostics;
using System.Runtime.InteropServices;
public class ModifierHandler : MonoBehaviour
{


    private int day;
    public VolumeProfile postprocessing;
    public GameObject camera;
    public float vignetteMaxValue;
    public float vignetteMinValue;
    private float vignetteCurrentValue = 0f;
    private Vignette vignette;
    public float vignetteModifier;
    private ChromaticAberration chromab;
    private LensDistortion lensdist;
    private int sign = 1;
    public GameObject sceneControllerObj;
    private SceneController sceneControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneControllerObj = GameObject.Find("Scene Controller");
        sceneControllerScript = sceneControllerObj.GetComponent<SceneController>();
        day = sceneControllerScript.CurrentDay;
        //get the modifiers from scenecontroller
        switch (day)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (day >= 2)
        {
            SleepyScreen();
        }
        if (day >= 3)
        {
            GuysOnScreen();
        }
    }
    void SleepyScreen()
    {
        if (postprocessing.TryGet(out vignette))
        {
            vignetteCurrentValue = Oscillate(Time.time, vignetteModifier);
            //vignette.intensity.value = vignetteCurrentValue;
            vignette.intensity.SetValue((new UnityEngine.Rendering.FloatParameter(vignetteCurrentValue)));
            print("VIGNETTE VALUE: " + (vignette.intensity.value));
        }
        if (postprocessing.TryGet(out chromab))
        {
            chromab.intensity.SetValue(new UnityEngine.Rendering.FloatParameter(vignetteCurrentValue / 2));
        }
        if (postprocessing.TryGet(out lensdist))
        {
            lensdist.intensity.SetValue(new UnityEngine.Rendering.FloatParameter(Mathf.Clamp(-(vignetteCurrentValue / 2), -0.6f, 0f)));
        }
    }
    Vector2 flySpawn1 = new Vector2(12f, -7.5f);
    Vector2 flySpawn2 = new Vector2(12f, 7.5f);
    Vector2 flySpawn3 = new Vector2(-12f, -7.5f);
    Vector2 flySpawn4 = new Vector2(-12f, 7.5f);
    public GameObject flyPrefab;
    public float flySpawnTime;
    float flySpawnTimer = 0f;
    void GuysOnScreen()
    {
        flySpawnTimer += 0.01f;
        if (flySpawnTimer > flySpawnTime)
        {
            int SPnum = UnityEngine.Random.Range(1, 4);
            Vector2 spawnPoint = new Vector2(-7.5f, -7.5f);
            if (SPnum == 1) { spawnPoint = flySpawn1; }
            else if (SPnum == 2) { spawnPoint = flySpawn2; }
            else if (SPnum == 3) { spawnPoint = flySpawn3; }
            else if (SPnum == 4) { spawnPoint = flySpawn4; }
            Instantiate(flyPrefab, spawnPoint, Quaternion.identity);
            flySpawnTimer = 0f;
        }
    }

    void OnDestroy()
    {
        vignette.intensity.SetValue((new UnityEngine.Rendering.FloatParameter(0)));
        chromab.intensity.SetValue(new UnityEngine.Rendering.FloatParameter(0));
        lensdist.intensity.SetValue(new UnityEngine.Rendering.FloatParameter(0));
    }
    float Oscillate(float time, float speed)
    {
        float amplitude = (0.6f - (-0.1f)) / 2f; // Half the total range
        float midpoint = (0.6f + (-0.1f)) / 2f;  // Midpoint of range

        return midpoint + amplitude * Mathf.Sin(time * speed);
    }

}

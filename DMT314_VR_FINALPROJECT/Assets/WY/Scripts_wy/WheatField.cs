using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheatField : MonoBehaviour
{
    [Header("Cultivation Field")]
    public List<GameObject> obstacles;
    public Material cultivatedMaterial;
    public Slider progressBar;
    public Text progressText;
    public GameObject cultivateUI;

    [Header("Sow Field")]
    public GameObject sowUI;
    public Slider wateringProgressBar;

    [Header("Watering Field")]
    public GameObject wateringUI;
    public Renderer fieldRenderer;
    public Color startColor, endColor;
    public float wateringDuration;
    public GameObject wateringEffectObject;

    private Coroutine wateringCoroutine;
    private Coroutine changeEngineColorCoroutine;
    private int totalObstacles;
    private bool isCultivated =false;
    private bool isWatered=false;
    // Start is called before the first frame update
    void Start()
    {

        if (obstacles == null || obstacles.Count == 0)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("WY_Obstacles")) // Tag all obstacles as "Obstacle"
                {
                    obstacles.Add(child.gameObject);
                }
            }
        }
        // count the obstacle
        totalObstacles = obstacles.Count;

        // Initialize the progresss bar
        if(progressBar != null)
        {
            progressBar.maxValue = totalObstacles;
            progressBar.value = 0;
        }

        // Initialize the progresss bar
        if (wateringProgressBar != null)
        {
            wateringProgressBar.maxValue = wateringDuration;
            wateringProgressBar.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DestroyAndUpdate();
    }
    private void DestroyAndUpdate()
    {
        int destroyedCount = 0;
        foreach(GameObject obstacle in obstacles)
        {
            if(obstacle == null)
            {
                destroyedCount++;
            }
        }
        progressBar.value = destroyedCount;

        // All destroyed
        if (!isCultivated && destroyedCount == totalObstacles)
        {
            Debug.Log("All destroyed");
            
            fieldRenderer.material = cultivatedMaterial;
            Wy_GameManager.instance.IncrementCultivatedFieldCount();

                // lock variable to avoid multiple update
             isCultivated =true;
            progressBar.gameObject.SetActive(false);
            progressText.text = "CULTIVATED";
        }

        if (Wy_GameManager.instance.cultivatedFieldCount == 4)
        {
            cultivateUI.SetActive(false);
            sowUI.SetActive(true);
        }
        if (Wy_GameManager.instance.sowedFieldCount == 4)
        {
            sowUI.SetActive(false);
            wateringUI.SetActive(true);
        }

        if (Wy_GameManager.instance.wateredFieldCount == 4)
        {
            wateringUI.SetActive(false);
            wateringEffectObject.SetActive(false);
        }
    }

    public void TriggerWateringEffect()
    {
        // Exit if already watered
        if (isWatered) return;

        // Mark the field as watered
        isWatered = true;


        if (changeEngineColorCoroutine != null)
            StopCoroutine(changeEngineColorCoroutine);
        wateringEffectObject.SetActive(true);
        // Start watering
        wateringCoroutine = StartCoroutine(WateringProgress());
        // Start Changing color
        changeEngineColorCoroutine = StartCoroutine(ChangeEngineColor(startColor, endColor, wateringDuration));

    }

    private IEnumerator WateringProgress()
    {
        float timeElapsed = 0f;

        while(timeElapsed< wateringDuration)
        {
            timeElapsed += Time.deltaTime;
            wateringProgressBar.value=timeElapsed;
            yield return null;
        }
        Wy_GameManager.instance.IncrementWateredFieldCount();
        
    }
    private IEnumerator ChangeEngineColor(Color _startColor, Color _endColor, float _duration)
    {
        // start tick 0f so that startColor is shown for a frame
        float tick = 0;
        while (tick < 1f)
        {
            // Update color
            fieldRenderer.material.color = Color.Lerp(_startColor, _endColor, tick);

            // Iterate tick after adjusting color so Lerp doesn't use a tick over 1f
            tick += Time.deltaTime / _duration;

            // Wait a frame
            yield return null;
        }
        // Ensure that the endColor is applied before exit
        fieldRenderer.material.color = _endColor;
    }
}

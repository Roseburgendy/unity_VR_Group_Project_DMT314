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


    [Header("Watering Field")]
    public Slider wateringProgressBar;
    public GameObject wateringUI;
    public Renderer fieldRenderer;
    public Color startColor, endColor;
    public float wateringDuration;
    public GameObject wateringEffectObject;
    public Text waterText;

    [Header("Wheat Growth")]
    public GameObject seeds;
    public List<GameObject> wheatStages;
    public float GrowthTime = 360f;
    public GameObject growingUI;
    public Slider growthProgressBar;
    public GameObject matureUI;

    private Coroutine wateringCoroutine;
    private Coroutine growthCoroutine;
    private Coroutine changeEngineColorCoroutine;
    private float currentWateringProgress = 0f; // Tracks the current progress
    private float growthElapsedTime = 0f; // Tracks growth progress
    private int currentStageIndex = 0;

    private int totalObstacles;
    private bool isCultivated =false;
    private bool isWatered=false;
    public bool isSowed = false;
    public bool isGrowing= false;
    private bool isMatured=false;
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

        // Initialize the growth progress bar
        if (growthProgressBar != null)
        {
            growthProgressBar.maxValue = 1f; // Normalized progress (0 to 1)
            growthProgressBar.value = 0f;
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
            cultivateUI.SetActive(false);
            sowUI.SetActive(true);
            Wy_GameManager.instance.IncrementCultivatedFieldCount();

                // lock variable to avoid multiple update
             isCultivated =true;
            progressBar.gameObject.SetActive(false);
            progressText.text = "CULTIVATED";
        }
        /*
        if (Wy_GameManager.instance.cultivatedFieldCount == 4)
        {
            cultivateUI.SetActive(false);
            sowUI.SetActive(true);
        }
        */
        if (Wy_GameManager.instance.wateredFieldCount == 4)
        {       
            wateringEffectObject.SetActive(false);
        }
    }

    public void StartWatering()
    {
        if (isWatered) return; // Exit if already watered
        if (!isSowed) return; // return if not sowed
        if(!isCultivated) return;// return if not cultivated

        // Enable watering effects
        wateringEffectObject.SetActive(true);

        // Start or resume the watering progress
        if (wateringCoroutine == null)
        {
            wateringCoroutine = StartCoroutine(WateringProgress());
        }

        // Start color transition
        if (changeEngineColorCoroutine == null)
        {
            changeEngineColorCoroutine = StartCoroutine(ChangeEngineColor(startColor, endColor, wateringDuration));
        }
    }
    public void StopWatering()
    {
        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
            wateringCoroutine = null;
        }

        // Optionally stop the color-changing coroutine if necessary
        if (changeEngineColorCoroutine != null)
        {
            StopCoroutine(changeEngineColorCoroutine);
            changeEngineColorCoroutine = null;
        }

        // Disable watering effects
        wateringEffectObject.SetActive(false);
    }
    private IEnumerator WateringProgress()
    {
        while (currentWateringProgress < wateringDuration)
        {
            currentWateringProgress += Time.deltaTime;

            // Update the progress bar
            if (wateringProgressBar != null)
            {
                wateringProgressBar.value = currentWateringProgress;
            }

            yield return null;
        }

        // Mark as watered when progress is complete
        CompleteWatering();
    }

    private void CompleteWatering()
    {
        isWatered = true;
        wateringProgressBar.gameObject.SetActive(false);
        waterText.text = "WATERED";

        // Update GameManager or perform additional logic
        Wy_GameManager.instance.IncrementWateredFieldCount();

        // Ensure effects are turned off
        wateringEffectObject.SetActive(false);

        Debug.Log("Watering completed!");
        wateringUI.SetActive(false);
        seeds.SetActive(false);
        wheatStages[currentStageIndex].SetActive(true);

        isGrowing = true;

        growingUI.SetActive(true);
        growthCoroutine = StartCoroutine(GrowthCycle(GrowthTime));
    }

    private IEnumerator GrowthCycle(float growthTime)
    {
        while (growthElapsedTime < growthTime)
        {
            growthElapsedTime += Time.deltaTime;

            // Update growth progress bar
            growthProgressBar.value = growthElapsedTime / growthTime;

            // Advance to the next stage based on progress
            int nextStageIndex = Mathf.FloorToInt((growthElapsedTime / growthTime) * (wheatStages.Count - 1));
            if (nextStageIndex != currentStageIndex)
            {
                wheatStages[currentStageIndex].SetActive(false);
                currentStageIndex = nextStageIndex;
                wheatStages[currentStageIndex].SetActive(true);
            }

            yield return null;
        }

        // Growth complete
        Debug.Log("Wheat is fully grown!");
        growthCoroutine = null;
        isMatured = true;
        matureUI.SetActive(true);
        // Hide the progress bar
        growingUI.SetActive(false);
    }
    private IEnumerator ChangeEngineColor(Color _startColor, Color _endColor, float _duration)
    {
        float elapsedTime = currentWateringProgress; // Start from current progress
        while (elapsedTime < _duration)
        {
            fieldRenderer.material.color = Color.Lerp(_startColor, _endColor, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fieldRenderer.material.color = _endColor;
    }
    public void Sowed()
    {
        isSowed = true;
    }

    public void MatureAllWheat()
    {
        if (growthCoroutine != null)
        {
            StopCoroutine(growthCoroutine);
        }

        // Instantly activate the final wheat stage
        wheatStages[currentStageIndex].SetActive(false);
        wheatStages[wheatStages.Count - 1].SetActive(true);
        growthProgressBar.value = GrowthTime;
        Debug.Log("Wheat instantly matured!");
        isGrowing = false;
        isMatured = true; 
        growingUI.SetActive(false);
        matureUI.SetActive(true);
    }
}

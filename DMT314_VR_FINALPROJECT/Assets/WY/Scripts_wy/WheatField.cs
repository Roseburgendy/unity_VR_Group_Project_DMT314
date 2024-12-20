using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheatField : MonoBehaviour
{
    public List<GameObject> obstacles;
    public Material cultivatedMaterial;
    public Slider progressBar;
    public Text progressText;
    public GameObject cultivateUI;
    public GameObject sowUI;

    public Renderer fieldRenderer;
    private int totalObstacles;

    private bool isCultivated =false;
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
    }
}

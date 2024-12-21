using UnityEngine;
using UnityEngine.UI;

public class TomatoBoxController : MonoBehaviour
{
    public Slider boxSlider;           // Slider 用于显示当前进度
    //public Transform boxArea;          // 盒子区域的 Transform
    public int maxEggs = 3;            // 最大蛋数
    public int currentEggs = 0;       // 当前蛋数

    void Start()
    {
        // 初始化 Slider
        if (boxSlider != null)
        {
            boxSlider.minValue = 0;
            boxSlider.maxValue = maxEggs;
            boxSlider.value = currentEggs;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入盒子区域的物体是否是蛋
        if (other.CompareTag("LX_Tomato"))
        {
            AddEgg(other.gameObject);
        }
    }

    void AddEgg(GameObject egg)
    {
        if (currentEggs < maxEggs)
        {
            currentEggs++;
            UpdateSlider();

            // 检查是否满进度
            if (currentEggs == maxEggs)
            {
                Debug.Log("Box is full!");
                OnBoxFull();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LX_Tomato"))
        {
            RemoveEgg(other.gameObject);
        }
    }

    void RemoveEgg(GameObject egg)
    {
        if (currentEggs > 0)
        {
            currentEggs--;
            UpdateSlider();

            Debug.Log($"Egg removed! Current count: {currentEggs}");
        }
    }

    void UpdateSlider()
    {
        if (boxSlider != null)
        {
            boxSlider.value = currentEggs;
        }
    }

    void OnBoxFull()
    {
        // 满进度时的行为
        Debug.Log("You have successfully added all eggs!");
        // 可以在这里播放动画、音效等。
    }
}

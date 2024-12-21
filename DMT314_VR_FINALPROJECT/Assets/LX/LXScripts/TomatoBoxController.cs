using UnityEngine;
using UnityEngine.UI;

public class TomatoBoxController : MonoBehaviour
{
    public Slider boxSlider;           // Slider ������ʾ��ǰ����
    //public Transform boxArea;          // ��������� Transform
    public int maxEggs = 3;            // �����
    public int currentEggs = 0;       // ��ǰ����

    void Start()
    {
        // ��ʼ�� Slider
        if (boxSlider != null)
        {
            boxSlider.minValue = 0;
            boxSlider.maxValue = maxEggs;
            boxSlider.value = currentEggs;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������������������Ƿ��ǵ�
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

            // ����Ƿ�������
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
        // ������ʱ����Ϊ
        Debug.Log("You have successfully added all eggs!");
        // ���������ﲥ�Ŷ�������Ч�ȡ�
    }
}

using UnityEngine;
using UnityEngine.UI;

public class FeedChicken : MonoBehaviour
{
    public Transform chickenFeeder;  // ��ʳ�۵� Transform
    public Transform food;  // ��ʳ�۵� Transform
    public float fillSpeed = 1f;   // ����ٶ�
    public float maxFill = 1f;       // ʳ������������0~1��
    public float currentFill = 0f;  // ��ǰ�����
    private Vector3 lastPosition;    // ���ϵ���һ֡λ��
    public Slider fillSlider;
    //public GameObject particleSystemPrefab; // ����ϵͳԤ����
    //public Transform particleSpawnPoint;  // ����ϵͳ����λ��

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // ��������Ƿ���ʳ���Ϸ�
        if (IsOverFeeder())
        {
            // ��������ƶ����ٶ�
            float movement = Mathf.Abs(transform.position.x - lastPosition.x);

            // �����ƶ������ʳ��
            currentFill = Mathf.Clamp(currentFill + movement * fillSpeed, 0, maxFill);

            // ����ʳ�۵����Ч��
            UpdateFeederVisual(currentFill);

            // ���� Slider ��ֵ
            UpdateSlider(currentFill);

        }

        // ������һ֡��λ��
        lastPosition = transform.position;
    }

    bool IsOverFeeder()
    {
        // �򵥼�������Ƿ��ڼ�ʳ�۵ķ�Χ��
        Vector3 feederPosition = chickenFeeder.position;
        float feederLength = 3.3f; // ��ʳ�۵Ŀ�ȣ�����ʵ��ģ�͵�����
        float feederWidth = 0.7f; // ��ʳ�۵Ŀ�ȣ�����ʵ��ģ�͵�����
        float feederHeight = 5f; // ��ʳ�۵ĸ߶�

        return Mathf.Abs(transform.position.x - feederPosition.x) < feederWidth / 2 &&
               Mathf.Abs(transform.position.z - feederPosition.z) < feederLength / 2 &&
               Mathf.Abs(transform.position.y - feederPosition.y) < feederHeight;
    }

    public void UpdateFeederVisual(float fillAmount)
    {
        // ���� food ������� Y ��λ��
        float newY = Mathf.Lerp(-0.2f, 0.2f, fillAmount);

        // ���� food �����λ��
        Vector3 currentPosition = food.position;
        food.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // ������ٷֱ�
        Debug.Log($"Feeder Fill: {fillAmount * 100}%");

        // ��������ϵͳ��λ�õ�ָ�����ɵ�
        //GameObject spawnedParticle = Instantiate(particleSystemPrefab, particleSpawnPoint.position, Quaternion.identity);

        // ��������ϵͳ�Զ�����
        //Destroy(spawnedParticle, 2f);

    }

    public void UpdateSlider(float fillAmount)
    {
        // ��� Slider ���ڣ���������ֵ
        if (fillSlider != null)
        {
            fillSlider.value = fillAmount;
        }
    }

}
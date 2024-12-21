using UnityEngine;
using UnityEngine.UI;

public class FeedChicken : MonoBehaviour
{
    public Transform chickenFeeder;  // 鸡食槽的 Transform
    public Transform food;  // 鸡食槽的 Transform
    public float fillSpeed = 1f;   // 填充速度
    public float maxFill = 1f;       // 食槽最大填充量（0~1）
    public float currentFill = 0f;  // 当前填充量
    private Vector3 lastPosition;    // 饲料的上一帧位置
    public Slider fillSlider;
    //public GameObject particleSystemPrefab; // 粒子系统预制体
    //public Transform particleSpawnPoint;  // 粒子系统生成位置

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // 检测饲料是否在食槽上方
        if (IsOverFeeder())
        {
            // 检测左右移动的速度
            float movement = Mathf.Abs(transform.position.x - lastPosition.x);

            // 根据移动量填充食槽
            currentFill = Mathf.Clamp(currentFill + movement * fillSpeed, 0, maxFill);

            // 更新食槽的填充效果
            UpdateFeederVisual(currentFill);

            // 更新 Slider 的值
            UpdateSlider(currentFill);

        }

        // 更新上一帧的位置
        lastPosition = transform.position;
    }

    bool IsOverFeeder()
    {
        // 简单检查饲料是否在鸡食槽的范围内
        Vector3 feederPosition = chickenFeeder.position;
        float feederLength = 3.3f; // 鸡食槽的宽度（根据实际模型调整）
        float feederWidth = 0.7f; // 鸡食槽的宽度（根据实际模型调整）
        float feederHeight = 5f; // 鸡食槽的高度

        return Mathf.Abs(transform.position.x - feederPosition.x) < feederWidth / 2 &&
               Mathf.Abs(transform.position.z - feederPosition.z) < feederLength / 2 &&
               Mathf.Abs(transform.position.y - feederPosition.y) < feederHeight;
    }

    public void UpdateFeederVisual(float fillAmount)
    {
        // 计算 food 对象的新 Y 轴位置
        float newY = Mathf.Lerp(-0.2f, 0.2f, fillAmount);

        // 更新 food 对象的位置
        Vector3 currentPosition = food.position;
        food.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // 输出填充百分比
        Debug.Log($"Feeder Fill: {fillAmount * 100}%");

        // 设置粒子系统的位置到指定生成点
        //GameObject spawnedParticle = Instantiate(particleSystemPrefab, particleSpawnPoint.position, Quaternion.identity);

        // 设置粒子系统自动销毁
        //Destroy(spawnedParticle, 2f);

    }

    public void UpdateSlider(float fillAmount)
    {
        // 如果 Slider 存在，更新它的值
        if (fillSlider != null)
        {
            fillSlider.value = fillAmount;
        }
    }

}
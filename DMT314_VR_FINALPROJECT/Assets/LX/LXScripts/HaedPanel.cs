using UnityEngine;

public class HeadTiltPanelController : MonoBehaviour
{
    public Transform playerHead; // 玩家头部的 Transform，一般是摄像机
    public GameObject panel;     // 要激活/隐藏的面板
    public float activationAngle = 45f; // 激活面板的抬头角度（单位：度）
    public float deactivationAngle = 15f; // 隐藏面板的抬头角度（单位：度）
    public float distanceFromPlayer = 2f; // 面板到玩家的距离

    private bool isPanelActive = false; // 面板是否激活

    void Update()
    {
        // 获取玩家头部的本地旋转角度
        float headTiltX = playerHead.localEulerAngles.x;

        // 将角度规范化为 -180 到 180 范围
        if (headTiltX > 180)
        {
            headTiltX -= 360;
        }

        // 判断是否激活面板
        if (!isPanelActive && headTiltX < -activationAngle)
        {
            ActivatePanel();
        }
        // 判断是否隐藏面板
        else if (isPanelActive && headTiltX > -deactivationAngle)
        {
            DeactivatePanel();
        }
    }

    void ActivatePanel()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            PositionPanel();
            isPanelActive = true;
        }
    }

    void DeactivatePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            isPanelActive = false;
        }
    }

    void PositionPanel()
    {
        // 计算面板应该出现的位置
        Vector3 forwardDirection = playerHead.forward.normalized;
        Vector3 targetPosition = playerHead.position + forwardDirection * distanceFromPlayer;

        // 设置面板的位置
        panel.transform.position = targetPosition;

        // 让面板始终面向玩家
        panel.transform.LookAt(playerHead);

        // 修正面板的旋转，固定 X 轴为 -45 度
        panel.transform.rotation = Quaternion.Euler(-45, panel.transform.rotation.eulerAngles.y+180, panel.transform.rotation.eulerAngles.z);
    }
}

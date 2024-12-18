using UnityEngine;
using UnityEngine.UI;  // 用于 Slider 组件
using UnityEngine.XR.Interaction.Toolkit; // 用于 XR 交互
using System.Collections;

public class ButtonInteraction : MonoBehaviour
{
    public Slider countdownSlider;  // 倒计时进度条
    public GameObject targetObject; // 目标物体
    public float countdownTime = 2f; // 倒计时时间

    private bool isCountingDown = false; // 倒计时是否进行中

    // 启动倒计时
    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownRoutine());
        }
    }

    // 倒计时协程
    private IEnumerator CountdownRoutine()
    {
        isCountingDown = true;
        float currentTime = 0f;

        // 更新进度条
        while (currentTime < countdownTime)
        {
            currentTime += Time.deltaTime;
            countdownSlider.value = currentTime / countdownTime;  // 更新进度条的填充量
            yield return null;
        }

        // 倒计时结束后显示物体并放大
        if (targetObject != null)
        {
            targetObject.SetActive(true);  // 显示物体
            TargetObjectControl targetControl = targetObject.GetComponent<TargetObjectControl>();
            if (targetControl != null)
            {
                targetControl.ShowAndResizeObject();  // 调用放大物体的函数
            }
        }

        isCountingDown = false;  // 重置倒计时标志
    }
}


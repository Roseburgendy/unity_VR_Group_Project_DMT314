using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ForMeat : MonoBehaviour
{
    public Slider sliderProgress; // Slider 条形进度条
    public GameObject btn;         // 按钮对象
    public Transform bin;          // 存放Cube的盒子
    public Canvas specialCanvas;   // 特定的Canvas
    public Animator cowAnimator;   // 动画控制器

    private bool hovering;         // 是否悬停
    private float progress;        // 当前进度
    private bool finish;           // 是否完成了交互
    private bool deathTriggered;   // 是否触发了死亡动画

    private void Start()
    {
        // 监听悬停进入事件
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener((e) =>
        {
            if (finish)
                return;

            hovering = true;
            cowAnimator.SetTrigger("Get hit"); // 播放攻击动画
        });

        // 监听悬停退出事件
        GetComponent<XRSimpleInteractable>().hoverExited.AddListener((e) =>
        {
            if (finish)
                return;

            hovering = false;
            cowAnimator.SetTrigger("Idle"); // 恢复到 Idle 动画
        });
    }

    private void Update()
    {
        if (finish)
            return;

        // 更新进度条
        if (hovering)
        {
            progress += Time.deltaTime * 0.5f;
        }
        else
        {
            progress -= Time.deltaTime * 0.5f; // 射线移开时进度条回退
            progress = Mathf.Clamp(progress, 0, 1); // 限制进度条范围在 0-1
        }

        sliderProgress.value = progress;

        // 如果进度条达到 3/4 并未触发死亡动画
        if (progress >= 0.75f && !deathTriggered)
        {
            deathTriggered = true;
            cowAnimator.SetTrigger("Death"); // 播放死亡动画
        }

        // 如果进度条完成
        if (progress >= 1)
        {
            sliderProgress.value = 1;
            finish = true;

            // 隐藏按钮
            btn.SetActive(false);

            // 判断第一个Cube是否激活
            bool first = !bin.transform.GetChild(0).gameObject.activeSelf;

            if (first)
            {
                bin.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                bin.transform.GetChild(1).gameObject.SetActive(true);
            }

            // 检测盒子中激活的Cube数量
            CheckBinStatus();

            // 延迟移除牛对象
            StartCoroutine(RemoveCowAfterDelay(2.0f)); // 设置死亡动画的播放时间为 2 秒
        }
    }

    private void CheckBinStatus()
    {
        int activeCubeCount = 0;

        // 遍历bin的子对象，统计激活的Cube数量
        foreach (Transform child in bin)
        {
            if (child.gameObject.activeSelf)
            {
                activeCubeCount++;
            }
        }

        // 如果激活的Cube数量达到2，显示特定的Canvas
        if (activeCubeCount >= 2)
        {
            specialCanvas.gameObject.SetActive(true);
        }
    }

    private IEnumerator RemoveCowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // 移除牛对象
    }
}

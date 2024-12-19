using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ForMeat : MonoBehaviour
{
    public Image img_ring;         // 环形进度条
    public GameObject btn;         // 按钮对象
    public Transform bin;          // 存放Cube的盒子
    public Canvas specialCanvas;   // 特定的Canvas
    private bool hovering;         // 是否悬停
    private float progress;        // 当前进度
    private bool finish;           // 是否完成了交互

    private void Start()
    {
        // 监听悬停进入事件
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener((e) =>
        {
            if (finish)
                return;
            btn.SetActive(false);
            hovering = true;
        });

        // 监听悬停退出事件
        GetComponent<XRSimpleInteractable>().hoverExited.AddListener((e) =>
        {
            if (finish)
                return;
            hovering = false;
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
        img_ring.fillAmount = progress;

        // 如果进度条完成
        if (progress >= 1)
        {
            img_ring.fillAmount = 0;
            finish = true;

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
}

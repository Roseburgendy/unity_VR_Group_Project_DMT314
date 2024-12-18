using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectControl : MonoBehaviour
{
    public GameObject targetObject;  // 目标物体
    private bool isObjectVisible = false;  // 标记物体是否已显示
    private bool isSizeChanged = false;  // 标记物体大小是否已改变

    void Start()
    {
        // 初始时隐藏物体
        targetObject.SetActive(false);
    }

    // 显示物体并放大
    public void ShowAndResizeObject()
    {
        if (!isObjectVisible)
        {
            targetObject.SetActive(true);  // 显示物体
            isObjectVisible = true;
        }

        if (!isSizeChanged)
        {
            targetObject.transform.localScale = new Vector3(1, 2, 1);  // 将 Y 轴大小变为原来的两倍
            isSizeChanged = true;
        }
    }
}


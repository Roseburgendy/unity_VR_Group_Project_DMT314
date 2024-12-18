using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceArea : MonoBehaviour
{
    public meatCowChase cowChaseScript;  // 牛追逐脚本
    private bool isInFence = false;  // 判断牛是否在栅栏内
    private bool hasStopped = false;  // 标记是否已经执行过停止追逐的协程

    private void OnTriggerEnter(Collider other)
    {
        // 当牛进入栅栏区域
        if (other.CompareTag("MeatCow"))
        {
            Debug.Log("牛进入栅栏区域");
            isInFence = true;  // 标记牛进入栅栏区域
            hasStopped = false; // 重置停止状态
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当牛离开栅栏区域
        if (other.CompareTag("MeatCow"))
        {
            Debug.Log("牛离开栅栏区域");
            isInFence = false;  // 标记牛离开栅栏区域
            hasStopped = false; // 重置停止状态
        }
    }

    private void Update()
    {
        // 如果牛进入栅栏，并且协程还没有执行过
        if (isInFence && !hasStopped)
        {
            Debug.Log("牛在栅栏区域内");
            StartCoroutine(StopChasingAfterDelay());
            hasStopped = true;  // 设置已经执行过协程，防止重复调用
        }
    }

    // 延迟0.5秒后停止牛的追逐
    IEnumerator StopChasingAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);  // 等待0.5秒
        Debug.Log("牛停止追逐");
        cowChaseScript.StopChasing();  // 停止追逐
    }
}

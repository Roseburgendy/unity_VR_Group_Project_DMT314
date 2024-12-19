using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // 获取指向主角的方向向量
            Vector3 direction = player.position - transform.position;

            // 保证物体只在水平面上旋转，忽略Y轴高度差
            direction.y = 0;

            // 如果方向向量有效（非零向量）
            if (direction.sqrMagnitude > 0.01f)
            {
                // 计算目标旋转
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 绕Y轴增加180度的旋转
                targetRotation *= Quaternion.Euler(0, 180, 0);

                // 应用旋转
                transform.rotation = targetRotation;
            }
        }
    }
}

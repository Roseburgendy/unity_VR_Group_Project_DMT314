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
            // ��ȡָ�����ǵķ�������
            Vector3 direction = player.position - transform.position;

            // ��֤����ֻ��ˮƽ������ת������Y��߶Ȳ�
            direction.y = 0;

            // �������������Ч������������
            if (direction.sqrMagnitude > 0.01f)
            {
                // ����Ŀ����ת
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // ��Y������180�ȵ���ת
                targetRotation *= Quaternion.Euler(0, 180, 0);

                // Ӧ����ת
                transform.rotation = targetRotation;
            }
        }
    }
}

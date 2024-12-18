using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectControl : MonoBehaviour
{
    public GameObject targetObject;  // Ŀ������
    private bool isObjectVisible = false;  // ��������Ƿ�����ʾ
    private bool isSizeChanged = false;  // ��������С�Ƿ��Ѹı�

    void Start()
    {
        // ��ʼʱ��������
        targetObject.SetActive(false);
    }

    // ��ʾ���岢�Ŵ�
    public void ShowAndResizeObject()
    {
        if (!isObjectVisible)
        {
            targetObject.SetActive(true);  // ��ʾ����
            isObjectVisible = true;
        }

        if (!isSizeChanged)
        {
            targetObject.transform.localScale = new Vector3(1, 2, 1);  // �� Y ���С��Ϊԭ��������
            isSizeChanged = true;
        }
    }
}


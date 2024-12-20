using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ForMilk : MonoBehaviour
{
    public Slider sliderProgress; // Slider ���ν�����
    public GameObject btn;         // ��ť����
    public Transform bin;          // ���Cube�ĺ���
    public Canvas specialCanvas;   // �ض���Canvas
    private bool hovering;         // �Ƿ���ͣ
    private float progress;        // ��ǰ����
    private bool finish;           // �Ƿ�����˽���

    private void Start()
    {
        // ������ͣ�����¼�
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener((e) =>
        {
            if (finish)
                return;
            hovering = true;
        });

        // ������ͣ�˳��¼�
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

        // ���½�����
        if (hovering)
        {
            progress += Time.deltaTime * 0.5f;
        }
        sliderProgress.value = progress;

        // ������������
        if (progress >= 1)
        {
            sliderProgress.value = 1;
            finish = true;

            // ���ذ�ť
            btn.SetActive(false);

            // �жϵ�һ��Cube�Ƿ񼤻�
            bool first = !bin.transform.GetChild(0).gameObject.activeSelf;

            if (first)
            {
                bin.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                bin.transform.GetChild(1).gameObject.SetActive(true);
            }

            // �������м����Cube����
            CheckBinStatus();
        }
    }

    private void CheckBinStatus()
    {
        int activeCubeCount = 0;

        // ����bin���Ӷ���ͳ�Ƽ����Cube����
        foreach (Transform child in bin)
        {
            if (child.gameObject.activeSelf)
            {
                activeCubeCount++;
            }
        }

        // ��������Cube�����ﵽ2����ʾ�ض���Canvas
        if (activeCubeCount >= 2)
        {
            specialCanvas.gameObject.SetActive(true);
        }
    }
}

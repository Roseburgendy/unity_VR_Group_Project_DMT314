using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ForMeat : MonoBehaviour
{
    public Slider sliderProgress; // Slider ���ν�����
    public GameObject btn;         // ��ť����
    public Transform bin;          // ���Cube�ĺ���
    public Canvas specialCanvas;   // �ض���Canvas
    public Animator cowAnimator;   // ����������

    private bool hovering;         // �Ƿ���ͣ
    private float progress;        // ��ǰ����
    private bool finish;           // �Ƿ�����˽���
    private bool deathTriggered;   // �Ƿ񴥷�����������

    private void Start()
    {
        // ������ͣ�����¼�
        GetComponent<XRSimpleInteractable>().hoverEntered.AddListener((e) =>
        {
            if (finish)
                return;

            hovering = true;
            cowAnimator.SetTrigger("Get hit"); // ���Ź�������
        });

        // ������ͣ�˳��¼�
        GetComponent<XRSimpleInteractable>().hoverExited.AddListener((e) =>
        {
            if (finish)
                return;

            hovering = false;
            cowAnimator.SetTrigger("Idle"); // �ָ��� Idle ����
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
        else
        {
            progress -= Time.deltaTime * 0.5f; // �����ƿ�ʱ����������
            progress = Mathf.Clamp(progress, 0, 1); // ���ƽ�������Χ�� 0-1
        }

        sliderProgress.value = progress;

        // ����������ﵽ 3/4 ��δ������������
        if (progress >= 0.75f && !deathTriggered)
        {
            deathTriggered = true;
            cowAnimator.SetTrigger("Death"); // ������������
        }

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

            // �ӳ��Ƴ�ţ����
            StartCoroutine(RemoveCowAfterDelay(2.0f)); // �������������Ĳ���ʱ��Ϊ 2 ��
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

    private IEnumerator RemoveCowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // �Ƴ�ţ����
    }
}
